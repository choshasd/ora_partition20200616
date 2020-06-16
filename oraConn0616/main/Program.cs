using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oraConn0616
{
      class Program
      {
            static void Main(string[] args)
            {
                  /*
                  DbManager.getInstance().dbConnect();
                  -------------------------------------------
                  DbManager db = new DbManager();
                  db.dbConnect();
                  -------------------------------------------
                  new DbManager().dbConnect();
                   */

                  // 싱글톤으로 생성
                  DbManager.getInstance().dbConnect();


                  
                  while (true)
                  {

                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine(" 오라클 DB 관리 프로그램 v1.3");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("      1. 테이블 생성 ");
                        Console.WriteLine("      2. 테이블 삭제 ");
                        Console.WriteLine("      3. 데이터 추가 ");
                        Console.WriteLine("      4. 데이터 보기 ");
                        Console.WriteLine("      5. 데이터 수정 ");
                        Console.WriteLine("      6. 데이터 삭제 ");
                        Console.WriteLine("      7. 프로그램 종료 ");
                        Console.WriteLine();
                        Console.WriteLine("-----------------------------------");
                        Console.Write("메뉴선택 : ");
                        string menu = Console.ReadLine();

                        switch (menu)
                        {
                              case "1": // 테이블 생성
                                    DbManager.getInstance().createTable();
                                    break;
                              case "2": // 테이블 삭제
                                    DbManager.getInstance().dropTable();
                                    break;
                              case "3": // 데이터 추가
                                    DbManager.getInstance().insertDB();
                                    break;
                              case "4": // 데이터 보기
                                    DbManager.getInstance().showDB();
                                    break;
                              case "5": // 데이터 수정
                                    DbManager.getInstance().editDB();
                                    break;
                              case "6": // 데이터 삭제
                                    DbManager.getInstance().dropDB();
                                    break;
                              case "7": // 프로그램 종료
                                    Environment.Exit(0);
                                    break;
                              default:
                                    Console.WriteLine("오류");
                                    break;
                        }
                  }
            }
      }
}
