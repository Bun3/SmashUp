using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{

    public enum DIFFICULTY
    {
        DIFFICULTY_EASY = 4, DIFFICULTY_NORMAL = 7, DIFFICULTY_HARD = 15
    }

    public class CommandManager : MonoBehaviour
    {
        private Queue<int> patternQ;
        private Queue<int> inputQ;
        private int[] patternArr;
        private int[] inputArr;

        private int cmdCnt;

        private bool isInputTime = false;

        private Action startInputTime;
        private Action endInputTime;

        public Queue<int> PatternQ { get => patternQ; set => patternQ = value; }
        public Queue<int> InputQ { get => inputQ; set => inputQ = value; }
        public int[] InputArr { get => inputArr = inputQ.ToArray(); set => inputArr = value; }
        public int[] PatternArr { get => patternArr; set => patternArr = value; }
        public Action StartInputTime { get => startInputTime; set => startInputTime = value; }
        public Action EndInputTime { get => endInputTime; set => endInputTime = value; }
        public bool IsInputTime { get => isInputTime; set => isInputTime = value; }

        private void Awake()
        {
            patternQ = new Queue<int>();
            inputQ = new Queue<int>();
            patternQ.Clear();
            inputQ.Clear();

            startInputTime = () => { isInputTime = true; };
            endInputTime = () => { isInputTime = false; };
        }

        public DIFFICULTY GetRandomDifficulty(int e = 60, int n = 30, int h = 10)
        {
            int percent = UnityEngine.Random.Range(0, 100);
            if (percent < e)
                return DIFFICULTY.DIFFICULTY_EASY;
            else if (percent < e + n)
                return DIFFICULTY.DIFFICULTY_NORMAL;
            else if (percent < e + n + h)
                return DIFFICULTY.DIFFICULTY_HARD;
            else
            {
                print("Make Random Difficulty is Failed");
            }
            return DIFFICULTY.DIFFICULTY_EASY;
        }

        public Queue<int> GetRandomPattern(DIFFICULTY d)
        {
            patternQ.Clear();

            cmdCnt = (int)d;
            for (int i = 0; i < cmdCnt; i++)
            {
                patternQ.Enqueue(UnityEngine.Random.Range(1, 3));
            }

            patternArr = patternQ.ToArray();
            return patternQ;
        }

        public void PrintNowPattern()
        {
            string tmpStr = "Pattern : ";

            for (int i = 0; i < PatternArr.Length - 1; i++)
                tmpStr += PatternArr[i].ToString() + ", ";

            tmpStr += PatternArr[PatternArr.Length - 1].ToString();

            print("Count : " + patternQ.Count);
            print(tmpStr);
        }

        //현재 입력한 커맨드가 맞으면 
        public bool CompareNowInput()
        {
            if (InputArr[inputQ.Count - 1].Equals(patternQ.Peek()))
            {
                patternQ.Dequeue();
                return true;
            }
            return false;
        }

        //한 패턴의 입력이 모두 끝나면
        public bool CompareNowPattern()
        {
            if (inputQ.Count.Equals(cmdCnt) && patternQ.Count.Equals(0))
            {
                isInputTime = false;
                patternQ.Clear();
                inputQ.Clear();
                return true;
            }
            return false;
        }

    }

}