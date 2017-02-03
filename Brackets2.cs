using System;

namespace Algospot_BRACKETS2 {
    static class Program {
        class SimpleStack {
            int _top;
            int[] _array;

            public SimpleStack(int size) {
                _top = -1;
                _array = new int[size];
            }
            public void Push(int data) {
                // TOP의 값이 배열의 길이와 같거나 크면 데이터를 넣지 않는다.
                if (_top >= _array.Length) return;

                // 배열에 값을 집어넣는다.
                _array[++_top] = data;
            }
            public int Pop() {
                // TOP의 값이 0보다 작으면 데이터를 빼지 않는다.
                if (!CanPop) return -1;

                // 값을 뺀다
                return _array[_top--];
            }
            public bool CanPop {
                get { return _top >= 0; }
            }
            public bool IsEmpty {
                get { return _top == -1; }
            }
        }
        static void Main(string[] args) {
            // T 범위 제약
            int T = int.Parse(Console.ReadLine());
            if (T < 1 || T > 100) {
                Console.WriteLine("Range: 1 ≤ T ≤ 100");
                return;
            }

            // 결과를 저장할 배열
            bool[] solutions = new bool[T];

            // T 만큼 반복한다.
            for (int i = 0; i < T; i++) {

                // 입력 값을 받는다.
                string input = Console.ReadLine();

                // 길이 제약
                if (input.Length > 10000) {
                    Console.WriteLine("Range: string ≤ 10000");
                    continue;
                }

                // 열린/닫힌 괄호 상태를 저장하기 위한 배열
                int[] bracketStacks = new int[3];

                // 마지막으로 열린 괄호를 저장하기 위한 스택
                SimpleStack stack = new SimpleStack(input.Length);

                // 중도 오류 검사용
                bool error = false;
                for (int j = 0; j < input.Length; j++) {
                    // 문자열의 j 번째 위치에 있는 문자를 가져온다.
                    char ch = input[j];
                    
                    if (ch == '(') {
                        stack.Push(0);
                        bracketStacks[0]++;
                    }
                    else if (ch == '{') {
                        stack.Push(1);
                        bracketStacks[1]++;
                    }
                    else if (ch == '[') {
                        stack.Push(2);
                        bracketStacks[2]++;
                    }
                    else if (ch == ')') {
                        // 스택에서 값을 뺄 수 없는 경우
                        // 또는 해당 괄호다 다 닫힌 경우엔 종료한다.
                        if (!stack.CanPop || (bracketStacks[0] <= 0)) {
                            error = true;
                            break;
                        }

                        // 스택에서 값을 빼고 검사한다.
                        int lastBracket = stack.Pop();

                        // 괄호가 다르다면 종료한다.
                        if (lastBracket != 0) {
                            error = true;
                            break;
                        }

                        // 값을 감소시킨다.
                        bracketStacks[0]--;
                    }
                    else if (ch == '}') {
                        // 스택에서 값을 뺄 수 없는 경우
                        // 또는 해당 괄호다 다 닫힌 경우엔 종료한다.
                        if (!stack.CanPop || (bracketStacks[1] <= 0)) {
                            error = true;
                            break;
                        }

                        // 스택에서 값을 빼고 검사한다.
                        int lastBracket = stack.Pop();

                        // 괄호가 다르다면 종료한다.
                        if (lastBracket != 1) {
                            error = true;
                            break;
                        }

                        // 값을 감소시킨다.
                        bracketStacks[1]--;
                    }
                    else if (ch == ']') {
                        // 스택에서 값을 뺄 수 없는 경우
                        // 또는 해당 괄호다 다 닫힌 경우엔 종료한다.
                        if (!stack.CanPop || (bracketStacks[2] <= 0)) {
                            error = true;
                            break;
                        }

                        // 스택에서 값을 빼고 검사한다.
                        int lastBracket = stack.Pop();

                        // 괄호가 다르다면 종료한다.
                        if (lastBracket != 2) {
                            error = true;
                            break;
                        }

                        // 값을 감소시킨다.
                        bracketStacks[2]--;
                    }
                }

                // 오류가 없다면 괄호 값을 검사한다.
                if (!error)
                    // 스택이 비어있고, 괄호 상태를 저장한 배열의 모든 값이 0이면 성공한 것임
                    solutions[i] = stack.IsEmpty && (bracketStacks[0] == 0 && bracketStacks[1] == 0 && bracketStacks[2] == 0);
            }

            // 결과 출력
            for (int i = 0; i < T; i++) {
                Console.WriteLine("Case #{0}: {1}", i, solutions[i] ? "YES" : "NO");
            }
        }
    }
}
