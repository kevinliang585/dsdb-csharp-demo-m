using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.dasudian.sdk.dsdb;
using MongoDB.Bson;
using System.Threading;


namespace mongo_csharp_test
{
    public class Demo
    {
        public static void Main(string[] args)
        {
            String user = "tester";
            String passwd = "testerPasswd";
            String remoteAddress = "192.168.1.31";
            int port = 27017;
            String authDatabaseName = "test";

            DsdbClient client = new DsdbClient.Builder(user, passwd, remoteAddress, port, authDatabaseName).Build();
            client.connect();

            // 插入数据    2017/9/15 17:55:54 DateTime.Now.ToString()
            
            //BsonDocument doc = new BsonDocument();
            //doc.Add("name", "今天吗");
            //doc.Add("age", 26);
            //doc.Add("nums", 24);
            //doc.Add("time", DateTime.Now.ToString());
            //client.put(authDatabaseName, "kevindemo0920", doc);

            // 以下为查询    

            DsdbSearchOption searchOption = new DsdbSearchOption();
            searchOption.condition =
                DsdbFilter.And(DsdbFilter.Eq("age", 25), DsdbFilter.Eq("name", "kevin111"));

            Dictionary<int, String> sortMap = new Dictionary<int, String>();
            //1 升序 ,-1 降序
            sortMap.Add(1, "age");
     //       sortMap.Add(-1, "time");

            searchOption.sort = sortMap;

            searchOption.rows = 5;
     //       searchOption.start = 2;
            String[] str = { "name", "age" };
            searchOption.returnFields = str;

            DsdbSearchResult dsdbSearchResult = client.search("test", "kevindemo0920", searchOption);
            foreach (BsonDocument item in dsdbSearchResult.results)
            {
                Console.WriteLine(item.ToString());
            }

           /*以下为更新*/
           
            //DsdbUpdateDefinitionBuilder dudb = new DsdbUpdateDefinitionBuilder();
            //client.updateOne("test", "student", DsdbFilter.And(DsdbFilter.Eq("count", 6), DsdbFilter.Eq("type", "Database")), dudb.Set("type", "kevin"));
            //client.updateMany("test", "kevindemo0920", DsdbFilter.Lt("age", 25), dudb.Set("type", "kevin02"));


            // 以下为删除 
            //client.deleteMany("test", "student", DsdbFilter.Eq("qty", 45));

            Thread.Sleep(10000);

        }
    }
}
