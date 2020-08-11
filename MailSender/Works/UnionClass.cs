using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Infrastructure.Interfaces;

namespace MailSender.Works
{
    internal static class UnionClass
    {
        private static readonly Dictionary<string, object> _mergClasses = new Dictionary<string, object>();
        public static Dictionary<string, object> MergClasses { get => _mergClasses;}

        public static bool AddMergClass(IMergeableClass mergClass, string name = "")
        {
            if (mergClass is null) 
                return false;
            if (name == "")
                name = mergClass.Name;
            if (name == "") return false;
            if (MergClasses.ContainsKey(name))
                MergClasses[name] = mergClass;
            else
                MergClasses.Add(name, mergClass);
            return true;
        }

        public static void RemoveMergClass(IMergeableClass mergClass, string name = "")
        {
            if (mergClass is null) return;
            if (name == "")
                name = mergClass.Name;
            if (name == "") return;
            MergClasses.Remove(name);
        }
    }
}
