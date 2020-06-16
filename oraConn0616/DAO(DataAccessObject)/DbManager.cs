using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oraConn0616
{
      class DbManager
      {
            static DbManager instance;
            static string ORADB = "Data Source=(DESCRIPTION=(ADDRESS_LIST=" +
                "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                "User Id=hr;Password=1234;";

            OracleConnection conn;
            OracleCommand cmd;
            OracleDataReader dr;

            // 싱글톤
            public static DbManager getInstance()
            {
                  if (instance == null)
                  {
                        instance = new DbManager();
                  }
                  return instance;
            }

            // 기본생성자
            public DbManager()
            {
                  conn = new OracleConnection(ORADB);
                  cmd = new OracleCommand();
            }

            // 소멸자
            ~DbManager()
            {
                  dbClose();
            }

            public void dbConnect()
            {
                  try
                  {                        
                        conn.Open();
                        Console.WriteLine("오라클 접속 성공 !!");
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("오라클 접속오류 : " + oe.Message);
                        return;
                  }
            }
            public void dbClose()
            {
                  try
                  {
                        conn.Clone();
                        Console.WriteLine("오라클 접속 해제 !!");
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("오라클 해제 오류 : " + oe.Message);
                  }
            }
            // 1. 
            public void createTable()
            {
                  try
                  {

                        string query = "create table bigdata1(" + "id number not null, " + "name varchar(20) not null, " +
                              "age number not null, " + "addr varchar(80) not null, " + "constraint pk_bigdata1_id primary key(id))";
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        string query2 = "create sequence seq_id increment " +
                              "by 1 start with 1"; // 1부터 시작해서 1씩 증가
                        cmd.CommandText = query2;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("테이블 및 시퀀스 생성 성공 !!");
                  }catch(OracleException oe)
                  {
                        Console.WriteLine("테이블 생성 에러번호 :  "+oe.Number);
                        Console.WriteLine("테이블 생성 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("테이블 생성 에러메세지 :  " + oe.Message);
                  }

            }
            // 2.
            public void dropTable()
            {
                  try
                  {
                        string query = "drop table bigdata1";
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        string query2 = "drop sequence seq_id";
                        cmd.CommandText = query2;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("테이블 및 시퀀스 삭제 완료 !!");
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("테이블 삭제 에러번호 :  " + oe.Number);
                        Console.WriteLine("테이블 삭제 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("테이블 삭제 에러메세지 :  " + oe.Message);
                  }
            }
            // 3.
            public void insertDB()
            {
                  try
                  {

                        Console.Write("이름입력 : ");
                        string name = Console.ReadLine();

                        Console.Write("나이입력 : ");
                        string strAge = Console.ReadLine();
                        int age = Convert.ToInt32(strAge);

                        Console.Write("주소입력 : ");
                        string addr = Console.ReadLine();

                        string query = string.Format("insert into bigdata1 values (seq_id.nextval, '{0}', '{1}', '{2}')", name, age, addr);
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("데이터 입력 성공 !!");
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("데이터 입력 에러번호 :  " + oe.Number);
                        Console.WriteLine("데이터 입력 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("데이터 입력 에러메세지 :  " + oe.Message);
                  }
            }
            // 4.
            public void showDB()
            {
                  try
                  {
                        int count = 1;
                        cmd.Connection = conn;
                        cmd.CommandText = "select * from bigdata1 order by id asc";
                        cmd.CommandType = System.Data.CommandType.Text;
                        OracleDataReader odr = cmd.ExecuteReader();

                        if (odr.HasRows)
                        {
                              while (odr.Read())
                              {
                                    Console.WriteLine("-----------------------------------");
                                    Console.WriteLine("번호 : " + count);
                                    Console.WriteLine("이름 : " + odr["name"]);
                                    Console.WriteLine("나이 : " + odr["age"]);
                                    Console.WriteLine("주소 : " + odr["addr"]);
                                    Console.WriteLine("-----------------------------------");
                                    count++;
                              }

                        }
                        else
                        {
                              Console.WriteLine("현재 데이터가 없습니다");
                        }
                        odr.Close();
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("데이터 보기 에러번호 :  " + oe.Number);
                        Console.WriteLine("데이터 보기 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("데이터 보기 에러메세지 :  " + oe.Message);
                  }
            }
            // 5.
            public void editDB()
            {
                  try
                  {
                        Console.Write("수정할 이름 : ");
                        string name = Console.ReadLine();
                        //Console.WriteLine();
                        Console.Write("수정할 나이 : ");
                        string strAge = Console.ReadLine();
                        int age = Convert.ToInt32(strAge);
                        Console.Write("수정할 주소 : ");
                        string addr = Console.ReadLine();
                        Console.Write("수정할 데이터 번호 : ");
                        string edit = Console.ReadLine();
                        int num = Convert.ToInt32(edit);

                        string query = string.Format("update bigdata1 set name = '{0}', age={1}, addr='{2}' where id=" + num, name, age, addr);
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("데이터 수정 에러번호 :  " + oe.Number);
                        Console.WriteLine("데이터 수정 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("데이터 수정 에러메세지 :  " + oe.Message);
                  }
            }
            // 6.
            public void dropDB()
            {
                  try
                  {
                        Console.Write("삭제할 데이터 번호를 입력하세요 : ");
                        string strNum = Console.ReadLine();
                        int num = Convert.ToInt32(strNum);

                        string query = "delete from bigdata1 where id=" + num;
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(num + "번째 데이터 삭제 성공");
                  }
                  catch (OracleException oe)
                  {
                        Console.WriteLine("데이터 삭제 에러번호 :  " + oe.Number);
                        Console.WriteLine("데이터 삭제 에러코드 :  " + oe.ErrorCode.ToString());
                        Console.WriteLine("데이터 삭제 에러메세지 :  " + oe.Message);
                  }
            }

      }
}
