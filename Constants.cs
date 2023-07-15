using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleShark
{
    public static class Constants
    {

        //public static short OpcodeEncryption = 44;
        //public static short OpcodeEncryption = 45; // v212.2
        public static short OpcodeEncryption = 47; // v242.2
        public static short ClientOpcodeEncryption = 109; // v242.2
        //public static short StartClientOp = 200;
        public static short StartClientOp = 207; // v212.2
        //public static short EndClientOp = 0x680;
        public static short EndClientOp = 0x761;
        public static string OpcodeEncryptionKey = "N3x@nGLEUH@ckEr!";
        //public static string OpcodeEncryptionKey => "M@PleStoryMaPLe!";
    }
}
