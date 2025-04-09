using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _NnBnH.Models;
using Npgsql;


/*
    LEGAL MENTION:
"PostgresSQL License":
        NpsqlEntityFrameworkCore (Through Nuget) 
        owned/developed by [The PostgreSQL Global Development Group/ The Regents of the University of California]
 
 */
namespace _NnBnH.MainNnBnH.ExternalProviders.DB
{
    class DB_PostgreSQLconnectionClass : DB_CommonDBConnectionClass
    {
        public DB_PostgreSQLconnectionClass(string ConnectionString) : base(ConnectionString)
        {
        }

        public override List<KanaGroupedLine> LondAllKanaFromDB(string KanaTableName, out Exception ProcessExcetion)
        {
            ProcessExcetion = default;



            int ProcessPercent = 0;

            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("Connection string is null or empty");

            try
            {
                List<KanaSymbol> KanaSeq = new List<KanaSymbol>();
                List<KanaGroupedLine> KanaGroupedLine = new List<KanaGroupedLine>();

                NpgsqlDataSource npgsqlDataSource = NpgsqlDataSource.Create(ConnectionString);
                using (NpgsqlCommand comm = npgsqlDataSource.CreateCommand($"SELECT * FROM {KanaTableName}"))
                {
                    using (NpgsqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KanaSeq.Add(
                                new KanaSymbol(
                                new KanaWritting(reader.GetString(0), reader.GetString(1)),
                                reader.GetString(2))
                                );

                        }
                    }
                }
                KanaLoadingPercentChnagedEventCaller(ProcessPercent += 25, ProcessPercent - 25, 25);

                var SortedKana = KanaSeq.GroupBy(count => count.Romanji.Length);
                List<KanaGroupedLine> KanaTable = new List<KanaGroupedLine>();

                KanaLoadingPercentChnagedEventCaller(ProcessPercent += 25, ProcessPercent - 25, 25);
                //TODO: Do something with it! Awful.
                #region Shitty grouping 
#pragma warning disable CS8604
                // a i u e o
                KanaTable.Add(new KanaGroupedLine("*",
                    SortedKana.ElementAtOrDefault(0)?.Where(kana => kana.Kana.Hiragana != "ん").ToList()));


                // ka ki ku ke ko
                KanaTable.Add(new KanaGroupedLine("k*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("k")).ToList()));

                // ga gi gu ge go
                KanaTable.Add(new KanaGroupedLine("g*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("g")).ToList()));


                // sa si su se so
                KanaTable.Add(new KanaGroupedLine("s*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("s")).ToList()));

                // za zi zu ze zo
                KanaTable.Add(new KanaGroupedLine("z*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("z")).ToList()));


                // ta-to
                KanaTable.Add(new KanaGroupedLine("t*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("t")).ToList()));
                // da-do
                KanaTable.Add(new KanaGroupedLine("d*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("d")).ToList()));


                // ra-ro
                KanaTable.Add(new KanaGroupedLine("r*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("r")).ToList()));


                // na-no
                KanaTable.Add(new KanaGroupedLine("n*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("n")).ToList()));


                // ma-mo
                KanaTable.Add(new KanaGroupedLine("m*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("m")).ToList()));


                KanaLoadingPercentChnagedEventCaller(ProcessPercent += 25, ProcessPercent - 25, 25);

                // ha-ho
                KanaTable.Add(new KanaGroupedLine("h*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("h")).ToList()));

                // pa-po
                KanaTable.Add(new KanaGroupedLine("p*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("p")).ToList()));

                // ba-bo
                KanaTable.Add(new KanaGroupedLine("b*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("b")).ToList()));

                // wa-wo
                KanaTable.Add(new KanaGroupedLine("w*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("w")).ToList()));

                // y*
                KanaTable.Add(new KanaGroupedLine("y*",
                    SortedKana.ElementAtOrDefault(1)?.Where(kana => kana.Romanji.StartsWith("y")).ToList()));
#pragma warning restore CS8604
                #endregion
                KanaLoadingPercentChnagedEventCaller(ProcessPercent += 25, ProcessPercent - 25, 25);

                KanaLoadedEventCaller(
                    KanaSeq.Count,
                    new DB_ObjectFetchedEventArgs(
                    typeof(List<KanaGroupedLine>),
                    KanaTable,
                    null,
                    DateTime.Now));

                //this.LoadedKanaEvent?.Invoke(this, KanaSeq.Count);
                return KanaGroupedLine;
            }
            catch (Exception ex)
            {
                KanaLoadedEventCaller(
                     0,
                     new DB_ObjectFetchedEventArgs(
                     ex.GetType(),
                     null,
                     ex,
                     DateTime.Now)
                     { failed = true });

                ProcessExcetion = ex;
                return [];
            }
        }

        public override List<Kanji> LoadAllKanjiFromDB(string KanjiTableName, out Exception ProcessExcetion)
        {
            throw new NotImplementedException();
            ProcessExcetion = null;



            int ProcessPercent = 0;

            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("Connection string is null or empty");

            try
            {
                List<Kanji> KanaSeq = new List<Kanji>();
                List<KanaGroupedLine> KanaGroupedLine = new List<KanaGroupedLine>();

                NpgsqlDataSource npgsqlDataSource = NpgsqlDataSource.Create(ConnectionString);
                using (NpgsqlCommand comm = npgsqlDataSource.CreateCommand($"SELECT * FROM {KanjiTableName}"))
                {
                    using (NpgsqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
