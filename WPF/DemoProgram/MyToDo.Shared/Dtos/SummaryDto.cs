using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyToDo.Shared.Dtos
{
   public class SummaryDto :BaseDto
    {
        private int sum;

        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        private int finishedCount;

        public int FinishedCount
        {
            get { return finishedCount; }
            set { finishedCount = value; }
        }
        private int memoCount;

        public int MemoCount
        {
            get { return memoCount; }
            set { memoCount = value; }
        }
        private string finishedRatio;

        public string FinishedRatio
        {
            get { return finishedRatio; }
            set { finishedRatio = value; }
        }

        private ObservableCollection<ToDoDto> toDoList;

        public ObservableCollection<ToDoDto> ToDoList
        {
            get { return toDoList; }
            set { toDoList = value; }
        }
        private ObservableCollection<MemoDto> memoList;

        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; }
        }
    }
}
