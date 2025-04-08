using _NnBnH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.MainNnBnH.ExternalProviders.WebAPI
{
    public abstract class WebAPIconnection
    {

        public static readonly HttpClient HttpProvider = new HttpClient();

        public delegate void ConnectionEstamblished();
        public delegate void KanaTableFetched();


        public event ConnectionEstamblished? ConnectionEstamblishedEvent;
        public event KanaTableFetched? KanaTableEstamblishedEvent;


        protected void KanaTAbleFetchedCaller(ConnectionEstamblished est) => ConnectionEstamblishedEvent?.Invoke();

        public virtual async Task<List<Kanji>> FetchKanjiTableAsynct() => throw new NotImplementedException();
        public virtual async Task<List<KanaGroupedLine>> KanaTable() => throw new NotImplementedException();

    }
}
