using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NaijaStartupApp.Models
{
    public class NsuVariables
    {
        [Serializable]
        public class TemporaryVariables
        {
            public string string_var0 { get; set; }
            public string string_var1 { get; set; }
            public string string_var2 { get; set; }
            public string string_var3 { get; set; }
            public string string_var4 { get; set; }
            public string string_var5 { get; set; }
            public string string_var6 { get; set; }
            public string string_var7 { get; set; }
            public string string_var8 { get; set; }
            public string string_var9 { get; set; }
            public string string_var10 { get; set; }
            public string string_var11 { get; set; }
            public string string_var12 { get; set; }
            public string string_var13 { get; set; }
            public string string_var14 { get; set; }
            public string string_var15 { get; set; }
            public string string_var16 { get; set; }
            public string string_var17 { get; set; }
            public int int_var0 { get; set; }
            public int int_var1 { get; set; }
            public int int_var2 { get; set; }

            public Int64 Int64_var0 { get; set; }
            public Int64 Int64_var1 { get; set; }
            public DateTime date_var0 { get; set; }
            public DateTime date_var1 { get; set; }
            public DateTime date_var2 { get; set; }
            public DateTime date_var4 { get; set; }
            public bool bool_var0 { get; set; }
            public bool bool_var1 { get; set; }
            public bool bool_var2 { get; set; }
            public bool bool_var3 { get; set; }
            public bool bool_var4 { get; set; }
            public bool bool_var5 { get; set; }

            public decimal decimal_var0 { get; set; }
            public decimal decimal_var1 { get; set; }
            public decimal decimal_var2 { get; set; }
            public decimal decimal_var3 { get; set; }
            public decimal decimal_var4 { get; set; }
            public decimal decimal_var5 { get; set; }
            public decimal[] date_array_temp0 { get; set; }
            public decimal[] date_array_temp1 { get; set; }
            public decimal[] date_array_temp2 { get; set; }
            public decimal[] date_array_temp3 { get; set; }
            public string[] string_array_temp0 { get; set; }
            public string[] string_array_temp1 { get; set; }
            public string[] string_array_temp2 { get; set; }
            public string[] string_array_temp3 { get; set; }
            public string[] string_array_temp4 { get; set; }
            public List<string> list_var0 { get; set; }

            public string ptitle { get; set; }
            public string idrep { get; set; }

            public string ApprovalLevelStatus { get; set; }
            public ChatModel ChatModel { get; set; }
            public IFormFile File1 { get; set; }
            public IFormFile File2 { get; set; }
            public IFormFile File3 { get; set; }
            public IFormFile File4 { get; set; }
            public IFormFile File5 { get; set; }
            public IFormFile File6 { get; set; }
            public IFormFile File7 { get; set; }
            public IFormFile File8 { get; set; }
            public IFormFile File9 { get; set; }
        }

        public class GlobalVariables
        {
            public int saltValue { get; set; }
            public string userid { get; set; }
            public string userName { get; set; }
            public int TenantId { get; set; }
            public string[] sarrayt1 { get; set; }
            public string ptitle { get; set; }

            public string RoleId { get; set; }
            public int ApprovalLevel { get; set; }

        }

        public class ChatModel
        {
            public List<ChatList> ViewChatList { get; set; }
            public List<ChatDetails> ViewChatDetails { get; set; }
            public string Response { get; set; }
            public class ChatList
            {
                public int TicketNumber { get; set; }
                public byte[] Image { get; set; }
                public string Status { get; set; }
                public int NoOfNew { get; set; }
                public DateTime Date { get; set; }
            }
            public class ChatDetails
            {
                public byte[] Image { get; set; }
                public string Message1 { get; set; }
                public string Message2 { get; set; }
                public string User { get; set; }
                
            }
        }

    }
}
