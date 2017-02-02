/*
 * Project: Algospot_RATIO
 * Author: Hye won, Hwang (devslaner@gmail.com)
 * Date: 2017-02-02
 */
using System;

namespace Algospot_RATIO {
	static class Program {
		// 최대 연승 수 (20억)
		const int MAXIMUM_WINNING_STREAK = 2000000000;
		
		// 조건 검사 당 건너뛸 수 (1만)
		const int NEXT_LOOP_DISTANCE = 10000;
		
		// 자세하게 검사할 때의 스텝 수 (2)
		const int SMALL_LOOP_STEP = 2;
		public static void Main(string[] args) {
			uint T, N, M;
			
			T = uint.Parse(Console.ReadLine());
			int[] solutions = new int[T];
			for (uint i = 0; i < T; i++) {
				string[] tokens = Console.ReadLine().Split(' ');
				N = uint.Parse(tokens[0]);
				M = uint.Parse(tokens[1]);
				
				// N, M이 같을 경우 -1
				if (N == M) {
					solutions[i] = -1;
					continue;
				}
				
				// 마지막 순환 인덱스를 저장하기 위한 변수
				uint lastIndex = 1;
				
				// 문제에 대한 답을 찾았는지 저장하기 위한 변수
				bool solved = false;
				
				// 최초 입력값에 대한 승률을 구한다.
				uint initialRatio = (uint) (((double)M / N) * 100.0);
				
				// -- 단순 순환
				for (uint j = 1; j <= MAXIMUM_WINNING_STREAK; j += NEXT_LOOP_DISTANCE) {
					
					// N, M 값을 새로 계산한 다음 승률을 다시 구한다.
					uint N1 = N + j, M1 = M + j;
					uint winRatio = (uint) (((double) M1 / N1) * 100.0);
					
					// 다시 계산된 승률이 최초 입력값에 대한 승률보다 클 경우
					if (winRatio > initialRatio) {
						
						// 마지막 값부터 SMALL_LOOP_STEP 씩 반복을 시작한다.
						// -- 정밀 순환
						for (uint k = lastIndex; k < (lastIndex + NEXT_LOOP_DISTANCE); k += SMALL_LOOP_STEP) {
							
							// N, M 값을 새로 계산한 다음 승률을 다시 구한다.
							uint N2 = N + k, M2 = M + k;
							uint winRatio2 = (uint) (((double) M2 / N2) * 100.0);
							
							// 다시 계산된 승률이 최초 입력값에 대한 승률보다 클 경우
							if (winRatio2 > initialRatio) {
								
								// 값을 1 감소시킨 다음 확률을 재-계산한다.
								N2--;
								M2--;
								winRatio2 = (uint) (((double) M2 / N2) * 100.0);
								
								// 답을 찾았다!
								solutions[i] = (int) ((winRatio2 > initialRatio) ? k - 1 : k);
								solved = true;
								break;
							}
						}
					}
					
					// 해결했다면 그냥 나간다.
					if (solved) break;
					
					// 마지막 순환 인덱스를 저장한다.
					lastIndex = j;
				}
				
				// 끝까지 순환했는데 답을 찾지 못했다면 나머지 값을 순환하며 찾는다.
				if (!solved) {
					
					// 마지막 순환 인덱스부터 MAXIMUM_WINNING_STREAK 까지
					// -- 정밀 순환
					for (uint k = lastIndex; k < lastIndex + MAXIMUM_WINNING_STREAK; k += SMALL_LOOP_STEP) {
						
						// N, M 값을 새로 계산한 다음 승률을 다시 구한다.
						uint N2 = N + k, M2 = M + k;
						uint winRatio2 = (uint) (((double) M2 / N2) * 100.0);
						
						// 다시 계산된 승률이 최초 입력값에 대한 승률보다 클 경우
						if (winRatio2 > initialRatio) {
							
							// 값을 1 감소시킨 다음 확률을 재-계산한다.
							N2--;
							M2--;
							winRatio2 = (uint) (((double) M2 / N2) * 100.0);
							
							// 답을 찾았다!
							solutions[i] = (int) ((winRatio2 > initialRatio) ? k - 1 : k);
							solved = true;
							break;
						}
					}
				}
			
				// 그래도 못찾았다면... 이거 실화냐?
				if (!solved) solutions[i] = -1;
			}
			
			Console.WriteLine("주어진 테스트 케이스에 대한 결과:");
			for (int i = 0; i < T; i++) {
				Console.WriteLine("Case #{0}: {1}", i, solutions[i]);
			}
		}
	}
}
