using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using PersonalFinanceRepository;

namespace Demo.Controllers {
    public class ResolveUser {
        public static User FromRequest(HttpContextBase context, int? UserId) {
            User result = null;

            if (UserId.HasValue) {
                return Users.GetById(UserId.Value);
            }
            else {
                var path = context.Request.Url.AbsolutePath;
                var regex = new Regex("/users/([^/]+)(/.*)?$", RegexOptions.IgnoreCase);
                var match = regex.Match(path);
                if (match.Success) {
                    var found = Users.All().Where(u => CleanUserName(u.Name) == CleanUserName(match.Groups[1].Value)).FirstOrDefault();
                    if (found != null && found.Id != 0) {
                        result = found;
                    }
                }
            }

            return result;
        }

        private static string CleanUserName(string name) {
            return name.Replace(" ", string.Empty).ToLowerInvariant();
        }
    }
}