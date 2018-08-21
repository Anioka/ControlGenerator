using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Control_Generator
{
    static class Program
    {
        //public static string userLoginQuery = "SELECT * FROM login";
        public static string userLoginQuery = "SELECT * FROM login WHERE username=?param1 AND password=?param2";
        public static string userELoginQuery = "SELECT * FROM login WHERE email=?param1";
        public static string userUpdateQueryPassword = "UPDATE login SET password=?param1 WHERE username=?param2";
        public static string userUpdateQueryUsername = "UPDATE login SET username=?param1 WHERE email=?param2";
        public static string userDeleteQuery = "DELETE FROM login WHERE username=?param1 && email=?param2";
        public static string userInsertIntoQuery = "INSERT INTO login (username, password, role, email) VALUES (?param1, ?param2, ?param3, ?param4)";


        public static string clampInsertIntoQuery = "INSERT INTO finallcontrol (clamptype, serialnum, rado, rn, controldate, barcode, rfcable, rfcablerror, solenoidcable, solenoidcablerror, smb, smberror, armature, armaturerror, edges, edgeserror, paralelity, paralelityerror, distance, distancerror, rflenght, rflenghterror, solenoidlenght, solenoidlenghterror, electrodes, electrodeserror, bigresistance, bigresistancerror, smallresistance, smallresistancerror, shortcircuit, shortcircuiterror, emptythick, emptythickerror, fullthick, fullthickerror, emptythin, emptythinerror, fullthin, fullthinerror) VALUES (?param1, ?param2, ?param3, ?param4, ?param5, ?param6, ?param7, ?param8, ?param9, ?param10, ?param11, ?param12, ?param13, ?param14, ?param15, ?param16, ?param17, ?param18, ?param19, ?param20, ?param21, ?param22, ?param23, ?param24, ?param25, ?param26, ?param27, ?param28, ?param29, ?param30, ?param31, ?param32, ?param33, ?param34, ?param35, ?param36, ?param37, ?param38, ?param39, ?param40)";
        public static string clampUpdateQueryBarcode = "UPDATE finallcontrol SET serialnum=?param1 WHERE serialnum=?param2";
        public static string clampDeleteQuery = "DELETE FROM finallcontrol WHERE id=(SELECT MAX(id) FROM (SELECT * FROM finallcontrol) AS clamptable)";
        public static string clampSelectQuery = "SELECT serialnum, rado, rn FROM finallcontrol WHERE id=(SELECT MAX(id) FROM (SELECT * FROM finallcontrol) AS clamptable);";
        public static string clampSelectPackingList = "SELECT serialnum, rado, clamptype FROM finallcontrol where substring(serialnum,5,10) >= substring(?param1,5,10) and substring(serialnum,5,10) <= substring(?param2,5,10) and substring(serialnum,1,4) = substring(?param1,1,4) and substring(serialnum,1,4) = substring(?param2,1,4)";
        public static string clampSelectRN = "SELECT rn FROM finallcontrol where substring(serialnum,5,10) >= substring(?param1,5,10) and substring(serialnum,5,10) <= substring(?param2,5,10) and substring(serialnum,1,4) = substring(?param1,1,4) and substring(serialnum,1,4) = substring(?param2,1,4)";
        public static string clampSelectPackingListCount = "SELECT COUNT(*) FROM finallcontrol where substring(serialnum,5,10) >= substring(?param1,5,10) and substring(serialnum,5,10) <= substring(?param2,5,10) and substring(serialnum,1,4) = substring(?param1,1,4) and substring(serialnum,1,4) = substring(?param2,1,4)";
        public static string clampSelectValidity = "SELECT * FROM finallcontrol WHERE serialnum=?param1";
        public static string clampSelectFinalControlReport = "SELECT serialnum, rado, clamptype, smallresistance, bigresistance FROM finallcontrol where substring(serialnum,5,10) >= substring(?param1,5,10) and substring(serialnum,5,10) <= substring(?param2,5,10) and substring(serialnum,1,4) = substring(?param1,1,4) and substring(serialnum,1,4) = substring(?param2,1,4)";


        
        public static string clampStatistics = "SELECT COUNT(*) AS ammount FROM finallcontrol WHERE clamptype='PC' AND controldate LIKE ?param1 UNION SELECT COUNT(*) AS ammount FROM finallcontrol WHERE clamptype='WH1 - 15V' AND controldate LIKE ?param1 UNION SELECT COUNT(*) AS ammount FROM finallcontrol WHERE clamptype='WH1 - 24V' AND controldate LIKE ?param1 UNION SELECT COUNT(*) AS ammount FROM finallcontrol WHERE clamptype='WH2 - 15V' AND controldate LIKE ?param1 UNION SELECT COUNT(*) AS ammount FROM finallcontrol WHERE clamptype='WH2 - 24V' AND controldate LIKE ?param1 ORDER BY ammount DESC";
        public static string clampStatisticsValues = "SELECT COUNT(*) AS ammount, clamptype AS name FROM finallcontrol WHERE clamptype='PC' AND controldate LIKE ?param1 HAVING COUNT(*) > 0 UNION SELECT COUNT(*) AS ammount, clamptype AS name FROM finallcontrol WHERE clamptype='WH1 - 15V' AND controldate LIKE ?param1 HAVING COUNT(*) > 0 UNION SELECT COUNT(*) AS ammount, clamptype AS name FROM finallcontrol WHERE clamptype='WH1 - 24V' AND controldate LIKE ?param1 HAVING COUNT(*) > 0 UNION SELECT COUNT(*) AS ammount, clamptype AS name FROM finallcontrol WHERE clamptype='WH2 - 15V' AND controldate LIKE ?param1 HAVING COUNT(*) > 0 UNION SELECT COUNT(*) AS ammount, clamptype AS name FROM finallcontrol WHERE clamptype='WH2 - 24V' AND controldate LIKE ?param1 HAVING COUNT(*) > 0 ORDER BY ammount DESC";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
