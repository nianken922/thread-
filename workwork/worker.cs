using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workwork
{//http://marklin-blog.logdown.com/posts/291268-object-oriented-series-menu-11-generic-generics
    delegate void Work_demo(int num);
    class Worker
    {
        Work_demo d;
        public event Work_demo TaskStart=null;
        public event Work_demo TaskDone=null;
        public int num,man;
        
        /// <summary>
        /// 計算數量，工人
        /// </summary>
        /// <param name="num"></param>
        /// <param name="man"></param>
        public Worker(int num,int man)
        {
            this.num = num;
            this.man = man;
        }
        /// <summary>
        /// 開始工作
        /// </summary>
        public void Start()
        {
            if (TaskStart != null)
            {
                TaskStart(num);
            }           
        }
        /// <summary>
        /// 完成工作
        /// </summary>
        public void Work()
        {
            
            if (TaskDone != null)
            {
                TaskDone(num);
            }
        }
        /// <summary>
        /// 傳回時間
        /// </summary>
        /// <returns></returns>
        public int GetWork()
        {
            return man;
        }
        public int Getnum()
        {
            return num;
        }
        public int Getint(int i)
        {
            
            return i;
        }
        


    }
}
