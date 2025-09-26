using System;
using System.Collections.Generic;
using System.Linq;
using AkelTestingTool.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Omu.ValueInjecter;

namespace AkelTestingTool.Models
{
    public static class Db
    {
        public static IList<AkelTestingTool.Models.TestsExeResults> TestsExeResults = new List<AkelTestingTool.Models.TestsExeResults>();


        public static int Gid = 151;

        public static object Set<T>()
        {
            if (typeof(T) == typeof(AkelTestingTool.Models.TestsExeResults)) return TestsExeResults;

            return null;
        }

        public static T Insert<T>(T o) where T : TestsExeResults
        {
            o.TERId2 = Gid += 2;
            ((IList<T>)Set<T>()).Add(o);
            return o;
        }

        public static T Get<T>(int? id) where T : TestsExeResults
        {
            var entity = ((IList<T>)Set<T>()).SingleOrDefault(o => o.TERId2 == id);
            if (entity == null) throw new Exception("this item doesn't exist anymore");
            return entity;
        }

        public static void Update<T>(T o) where T : TestsExeResults
        {
            var t = Get<T>(o.TERId2);
            t.InjectFrom(o);
        }

        public static void Delete<T>(int id) where T : TestsExeResults
        {
            ((IList<T>)Set<T>()).Remove(Get<T>(id));
        }




    }
}
          

            
       
        
        
       