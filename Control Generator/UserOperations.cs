using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Control_Generator
{
    //komunikacija user input - baza
    public static class UserOperations
    {
        public static User SelectUser(User user)
        {
            User usersvalues = new User();

            try
            {
                usersvalues = UserConnectionClass.SelectUser(Program.userLoginQuery, user);
            }
            catch (Exception ex)
            {
                //Debug.Write(ex);
            }

            return usersvalues;
        }

        public static User SelectUserMail(User user)
        {
            User usersvalues = new User();

            try
            {
                usersvalues = UserConnectionClass.SelectUserbyEmail(Program.userELoginQuery, user);
            }
            catch (Exception ex)
            {
                //Debug.Write(ex);
            }

            return usersvalues;
        }

        public static bool DeleteUser(User user)
        {
            bool result = false;
            bool checkout = false;
            User usersvalues = new User();

            try
            {
                usersvalues = UserConnectionClass.SelectUser(Program.userLoginQuery, user);

                checkout = usersvalues.DataV;

                if (checkout)
                {
                    result = UserConnectionClass.DeleteUser(Program.userDeleteQuery, user);
                }
                else result = false;
            }
            catch (Exception ex)
            {
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }

        public static bool UpdateUsername(User user)
        {
            bool result = false;
            bool checkout = false;
            User usersvalues = new User();           

            try
            {
                usersvalues = UserConnectionClass.SelectUserbyEmail(Program.userELoginQuery, user);

                checkout = usersvalues.DataV;
                if (checkout)
                {
                    result = UserConnectionClass.UpdateUserUsername(Program.userUpdateQueryUsername, user);
                }
                else result = false;
            }
            catch (Exception ex)
            {
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }

        public static bool UpdatePassword(User user, string newpassword)
        {
            bool result = false;
            bool checkout = false;
            User usersvalues = new User();

            try
            {
                usersvalues = UserConnectionClass.SelectUser(Program.userLoginQuery, user);

                checkout = usersvalues.DataV;
                if (checkout)
                {
                    user.Password = newpassword;

                    result = UserConnectionClass.UpdateUserPassword(Program.userUpdateQueryPassword, user);
                }
                else result = false;
            }
            catch (Exception ex)
            {
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }

        public static bool InsertUser(User user)
        {
            bool result = false;
            bool checkout = true;
            User usersvalues = new User();

            try
            {
                usersvalues = UserConnectionClass.SelectUser(Program.userLoginQuery, user);

                checkout = (bool)usersvalues.DataV;
                if (!checkout)
                {
                    result = UserConnectionClass.CreateUser(Program.userInsertIntoQuery, user);
                }
                else result = false;
            }
            catch (Exception ex) {
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }        
    }
}
