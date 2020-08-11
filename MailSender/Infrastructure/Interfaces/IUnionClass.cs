using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Infrastructure.Interfaces
{
    interface IUnionClass
    {
        Dictionary<string, object> MergClasses { get; }

        bool AddMergClass(IMergeableClass mergClass, string name = "");

        void RemoveMergClass(IMergeableClass mergClass, string name = "");
    }
}
