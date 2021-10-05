using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace workwork
{
    public partial class Form1 : Form
    {
        private List<Worker> WorkerList = new List<Worker>();
        private List<Worker> SecondList = new List<Worker>();
        private List<Worker> FinalList = new List<Worker>();
        public byte choose;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal value = numericUpDown1.Value;
            Random random = new Random(((int)DateTime.Now.Ticks));
            int num = WorkerList.Count + SecondList.Count + FinalList.Count + 1;
            for(int i = 0; i < value; i++)
            {
                int manT = random.Next(1, 60);
                Worker worker = new Worker(num++, manT);
                worker.TaskStart += Second;
                worker.TaskDone += Final;
                WorkerList.Add(worker);
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach(Worker worker3 in WorkerList)
            {
                stringBuilder.Append(worker3.num + "_" + worker3.GetWork() + "s" + "\r");
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(stringBuilder.ToString());
        }
        private void Second(int num)
        {
            Worker tmpWorker = null;

            for (int i = 0; i <= WorkerList.Count; i++)
            {
                if (WorkerList[i].num == num)
                {
                    tmpWorker = WorkerList[i];
                    WorkerList.RemoveAt(i);
                    break;
                }
            }
            SecondList.Add(tmpWorker);


            //decimal value = numericUpDown2.Value;
            ////foreach(workwork.Worker worker2 in WorkerList)
            //for(int i=0;i<=WorkerList.Count;i++)
            //{                
   
            //    if(SecondList.Count<=value) {
            //        SecondList.Add(WorkerList[i]);
            //        WorkerList.Remove(WorkerList[i]);
            //        break;
            //    }
                
                
               

            //}
                       
         }
        private void Final(int num)
        {
            ////這邊移下去會有問題
            ///不知道怎麼改成不需要使用FOR
            for (int i = 0; i < SecondList.Count; i++)
            {
                if (SecondList[i].num == num)
                {
                    FinalList.Add(SecondList[i]);
                    //SecondList.Remove(SecondList[i]);
                    SecondList.RemoveAt(i);
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            decimal value = numericUpDown2.Value;
            if (WorkerList.Count > 0)
            {
                for(int i = 0; i < value-SecondList.Count; i++)
                {
                    if (i < WorkerList.Count)
                    {
                        WorkerList[i].Start();
                    }
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach(workwork.Worker worker in WorkerList)
            {
                stringBuilder.Append(worker.num + "_" + worker.GetWork() + "s" + "\r");
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(stringBuilder.ToString());
            //跑出左邊


            if (SecondList.Count > 0)
            {
                for(int i = 0; i < SecondList.Count ; i++)
                {

                    int t = SecondList[i].GetWork();
                    if (t == 0)
                    {
                        SecondList[i].Work();
                    }
                }
            }
            //////這邊移上去後會怪怪的
            richTextBox2.Clear();
            foreach (workwork.Worker second in SecondList)
            {
                if (second.GetWork() >0)
                {
                    
                    second.man--;
                    
                }
              
                
                //沒變很可惜
                if (second.GetWork() < 20)
                {
                    richTextBox2.SelectionColor = Color.Purple;
                }else
                    richTextBox2.SelectionColor = Color.Blue;

                richTextBox2.AppendText(second.num + "_" + second.GetWork() + "s" + "\r");
            }          
            
    
            //跑出中間
            StringBuilder stringBuilder3 = new StringBuilder();
            foreach (Worker final in FinalList)
            {
               
                stringBuilder3.Append(final.num + "_" + final.GetWork() + "s" + "\r");
            }
            richTextBox3.Clear();
            richTextBox3.SelectionColor = Color.Red;
            richTextBox3.AppendText(stringBuilder3.ToString());
            //跑出右邊
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > 0)
            {
                timer1.Start();
            }
        }

        Thread myThread=null;
        Thread myThread2;
        Thread myThread3;
        Thread myThread_Timer;

        void StartThread()
        {          

            myThread = new Thread(new ThreadStart(Run_T1));
            myThread2 = new Thread(new ThreadStart(Run_T2));
            myThread3 = new Thread(new ThreadStart(Run_T1));
            myThread_Timer = new Thread(new ThreadStart(Run_Timer));

            myThread.Start();
            myThread2.Start();

            isRunTimer = true;
            myThread_Timer.Start();
            

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("StartThread:" + i);
                lock (sync_List)
                {
                    listSTR.Add("StartThread:" + i);
                }
                Thread.Sleep(50);
            }

        }

        delegate void DelString(string str);

        void ShowString_RB1(string s)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DelString(ShowString_RB1), new object[] { s});
            }
            else
            {
                this.richTextBox1.Text = s;
            }
        }


        object sync_List = new object();
        List<string> listSTR = new List<string>();
        void Run_T1()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Run_T1:" + i);
                lock (sync_List)
                {
                    listSTR.Add("Run_T1:" + i);
                }
                Thread.Sleep(20);
            }
            ShowString_RB1("RUN_T1 ... DONE");
        }

        void Run_T2()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Run_T2:" + i);
                Thread.Sleep(10);
            }
        }

        bool isRunTimer = true;
        void Run_Timer()
        {
            while (isRunTimer)
            {
                // TODO

                Thread.Sleep(200);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isRunTimer = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
