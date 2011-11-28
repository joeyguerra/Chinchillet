using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc.Resources;
using System.Web.Mvc;
namespace Chinchillet
{
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments",
        Justification = "The accessor is exposed as an ICollection<string>.")]
    [AspNetHostingPermission(System.Security.Permissions.SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class HttpAcceptedMethods : ActionMethodSelectorAttribute
    {
        public HttpAcceptedMethods(HttpVerbs verbs)
            : this(EnumToArray(verbs))
        {
        }

        public HttpAcceptedMethods(params string[] verbs)
        {
            if (verbs == null || verbs.Length == 0) throw new ArgumentException("NULL or Empty", "verbs");
            Verbs = new ReadOnlyCollection<string>(verbs);
        }

        public ICollection<string> Verbs
        {
            get;
            private set;
        }

        private static void AddEntryToList(HttpVerbs verbs, HttpVerbs match, List<string> verbList, string entryText)
        {
            if ((verbs & match) != 0) verbList.Add(entryText);
        }

        internal static string[] EnumToArray(HttpVerbs verbs)
        {
            var verbList = new List<string>();
            AddEntryToList(verbs, HttpVerbs.Get, verbList, "GET");
            AddEntryToList(verbs, HttpVerbs.Post, verbList, "POST");
            AddEntryToList(verbs, HttpVerbs.Put, verbList, "PUT");
            AddEntryToList(verbs, HttpVerbs.Delete, verbList, "DELETE");
            AddEntryToList(verbs, HttpVerbs.Head, verbList, "HEAD");
            AddEntryToList(verbs, HttpVerbs.Head, verbList, "TRACE");
            AddEntryToList(verbs, HttpVerbs.Head, verbList, "OPTIONS");
            return verbList.ToArray();
        }

        public override bool IsValidForRequest(ControllerContext context, MethodInfo methodInfo)
        {
            if (context == null) throw new ArgumentNullException("controllerContext");
            var httpMethod = context.HttpContext.Request.Form["_method"]
                ?? context.HttpContext.Request.Headers["X-HTTP-Method-Override"]
                ?? context.HttpContext.Request.HttpMethod;
            return Verbs.Contains(httpMethod, StringComparer.OrdinalIgnoreCase);
        }
    }
}