using _NnBnH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A common namespace for each DB actions
namespace _NnBnH.MainNnBnH.ExternalProviders.DB
{
    /// <summary>
    /// Common abstract class for each DB connection. Base class
    /// </summary>
    public abstract class DB_CommonDBConnectionClass
    {
        public virtual string ConnectionString { get; private set; }
        public DB_CommonDBConnectionClass(string ConnectionString)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString) || string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentException("Connection string can't be empty");
            this.ConnectionString = ConnectionString;
        }



        public delegate void LoadedKanaHandler(int KanaCount, DB_ObjectFetchedEventArgs dB_ObjectFetchedEventArgs);
        public event LoadedKanaHandler? LoadedKanaTableEvent;

        public delegate void KanaLoadingPercentHandler(int Percent, int Previous, int Step);
        public event KanaLoadingPercentHandler? KanaLoadingPercentChangedEvent;

        protected void KanaLoadedEventCaller(int CanaCount, DB_ObjectFetchedEventArgs dB_ObjectFetchedEventArgs)
        => LoadedKanaTableEvent?.Invoke(CanaCount, dB_ObjectFetchedEventArgs);
        protected void KanaLoadingPercentChnagedEventCaller(int Percent, int Previous, int Step)
        => KanaLoadingPercentChangedEvent?.Invoke(Percent, Previous, Step);
        public virtual List<KanaGroupedLine> LondAllKanaFromDB(string KanjiTableName, out Exception ProcessExcetion)
        {
            throw new NotImplementedException();
        }




        public delegate void LoadedKnajiTableHandler(int KanjiCount, DB_ObjectFetchedEventArgs dB_ObjectFetchedEventArgs);
        public event LoadedKnajiTableHandler? LoadedKanjiTableEvent;

        protected void LoadedKanjiTableEventCaller(int KanjiCount, DB_ObjectFetchedEventArgs dB_ObjectFetchedEventArgs)
            => LoadedKanjiTableEvent(KanjiCount, dB_ObjectFetchedEventArgs);

        public virtual List<Kanji> LoadAllKanjiFromDB(string KanaTableName, out Exception ProcessExcetion)
        {
            throw new NotImplementedException();
        }


    }
}
