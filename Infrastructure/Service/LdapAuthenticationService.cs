using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IService;
using Application.LDAP;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.Dtos.LogIn;

namespace Infrastructure.Service
{
    

   
    
        public class LdapAuthenticationService : ILdapAuthenticationService
        {
            private readonly LdapSettings _ldapSettings;

            public LdapAuthenticationService(IOptions<LdapSettings> ldapSettings)
            {
                _ldapSettings = ldapSettings.Value;
            }

            public bool Authenticate(string username, string password, out string userDn, out Dictionary<string, List<string>> attributes)
            {
                userDn = null;
                attributes = null;

                try
                {
                    using (var ldapConnection = new LdapConnection())
                    {
                        // الاتصال بخادم LDAP
                        ldapConnection.Connected(_ldapSettings.Server, _ldapSettings.Port);

                        // الربط الأولي باستخدام بيانات المسؤول
                        ldapConnection.Bind(_ldapSettings.BindDn, _ldapSettings.BindPassword);

                        // البحث عن المستخدم
                        string filter = string.Format(_ldapSettings.SearchFilter, username);
                        var search = ldapConnection.Search(
                            _ldapSettings.BaseDn,
                            LdapConnection.SCOPE_SUB,
                            filter,
                            null,
                            false
                        );

                        LdapEntry entry = null;
                        while (search.HasMore())
                        {
                            try
                            {
                                entry = search.Next();
                                break;
                            }
                            catch (LdapException)
                            {
                                continue;
                            }
                        }

                        if (entry == null)
                        {
                            return false; // لم يتم العثور على المستخدم
                        }

                        userDn = entry.DN;

                        // استخراج سمات المستخدم
                        attributes = new Dictionary<string, List<string>>();
                        foreach (LdapAttribute attribute in entry.GetAttributeSet())
                        {
                            var values = new List<string>();
                            foreach (object val in attribute.StringValueArray)
                            {
                                values.Add(val.ToString());
                            }
                            attributes.Add(attribute.Name.ToLower(), values);
                        }

                        // محاولة الربط باستخدام بيانات المستخدم (للتحقق من كلمة المرور)
                        try
                        {
                            ldapConnection.BindAsync(userDn, password);
                            return true; // تم التحقق من صحة المستخدم
                        }
                        catch
                        {
                            return false; // كلمة المرور خاطئة
                        }
                    }
                }
                catch
                {
                    return false; // خطأ في الاتصال بخادم LDAP
                }
            }

            public LdapUserDto GetUser(string username)
            {
                string userDn;
                Dictionary<string, List<string>> attributes;

                // نستخدم كلمة مرور وهمية لأننا نريد فقط استرجاع السمات
                // سيتم إرجاع السمات حتى لو فشلت المصادقة
                Authenticate(username, "dummy_password", out userDn, out attributes);

                // استخراج البيانات المطلوبة
                string email = attributes.GetValueOrDefault("mail")?.FirstOrDefault() ??
                              (attributes.GetValueOrDefault("email")?.FirstOrDefault() ?? $"{username}@hiast.sy");

                string name = attributes.GetValueOrDefault("cn")?.FirstOrDefault() ??
                             (attributes.GetValueOrDefault("name")?.FirstOrDefault() ?? username);

                string department = attributes.GetValueOrDefault("department")?.FirstOrDefault() ??
                                   (attributes.GetValueOrDefault("ou")?.FirstOrDefault() ?? "Unknown");

                return new LdapUserDto
                {
                    UserID = username,
                    Name = name,
                    Email = email,
                    Department = department,
                    Dn = userDn
                };
            }
        }
    
}
