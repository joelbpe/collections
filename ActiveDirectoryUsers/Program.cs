using System;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryUsers {
    class program {
        static void Main() {
            try {
                /* Refer to:
                 * https://msdn.microsoft.com/en-us/library/system.directoryservices.accountmanagement.principalcontext(v=vs.110).aspx
                 */
                using (var context = new PrincipalContext(ContextType.Domain, "yourdomain")) {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context))) {
                        using (var results = searcher.FindAll()) {
                            foreach (UserPrincipal u in results) {
                                Console.WriteLine("--------------------------------");
                                Console.WriteLine("Employee Id " + u.EmployeeId);
                                Console.WriteLine("Given Name " + u.GivenName);
                                Console.WriteLine("Email Address " + u.EmailAddress);
                                Console.WriteLine("--------------------------------");
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Console.Write(ex.ToString());
                Console.Read();
            }
        }
    }
}
