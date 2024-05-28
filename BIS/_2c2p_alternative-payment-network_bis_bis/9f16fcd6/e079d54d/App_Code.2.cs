#pragma checksum "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\TripleDES.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BBE563F0D0E41FBB6B9D2FD6B29D14CF19219480"

#line 1 "D:\2C2P\Alternative-Payment-Network\BIS\BIS\App_Code\TripleDES.cs"
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
/// <summary>
/// Summary description for TripleDES
/// </summary>
public class TripleDES
{

    //  private String key = "4094426978780845";
    private byte[] finalText = null;
    private byte[] cipherText = null;
    private BufferedBlockCipher cipher = null;
    private readonly string finalKey = ConfigurationManager.AppSettings["3DESFinalKey"].ToString(); 


    public String get3DESEncryptedMessage1(String clearText, String md5_1, String md5_2)
    {

        byte[] counter = { (byte)0x00, (byte)0x01 };

        //Start code by EEH
        String msg = stringToHex("  " + clearText + " ")
                + (md5_1 != null && !md5_1.Trim().Equals("", StringComparison.InvariantCultureIgnoreCase) ? md5_1 : "");



        byte[] almostCompletedMessageByte = hexToBytes(
                stringToHex("  " + clearText + " ")
                        + (md5_1 != null && !md5_1.Trim().Equals("", StringComparison.InvariantCultureIgnoreCase) ? md5_1 : "")
               + (md5_2 != null && !md5_2.Trim().Equals("", StringComparison.InvariantCultureIgnoreCase) ? stringToHex(" ") + md5_2 : ""));


        byte[] CompleteMessageByte = almostCompletedMessageByte;// + 2];


        cipherText = performEncrypt(this.hexToBytes(finalKey), CompleteMessageByte);
        String ctS = Encoding.ASCII.GetString(Hex.Encode(cipherText));


        return (ctS);

    }

    public String get3DESEncryptedMessage1(String clearText, String md5_1)
    {

        return this.get3DESEncryptedMessage1(clearText, md5_1, null);
    }

    public byte[] generateMD5(byte[] input)
    {
        String strMD5 = "";
        //  byte input[] = strValue.getBytes();
        MD5Digest md5 = new MD5Digest();

        md5.BlockUpdate(input, 0, input.Length);


        //md5.update(input, 0, input.length);

        byte[] digest = new byte[md5.GetDigestSize()];
        md5.DoFinal(digest, 0);
        // strMD5 = new String(Hex.encode(digest));
        //return strMD5;
        return digest;
    }

    private byte[] performEncrypt(byte[] key, byte[] ptBytes)
    {
        //    byte[] ptBytes = plainText.getBytes();

        cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(getEngineInstance()));

        String name = cipher.AlgorithmName;


        cipher.Init(true, new KeyParameter(key));

        byte[] rv = new byte[cipher.GetOutputSize(ptBytes.Length)];
        Console.WriteLine("Key Cipher  " + rv);

        int oLen = cipher.ProcessBytes(ptBytes, 0, ptBytes.Length, rv, 0);
        try
        {
            cipher.DoFinal(rv, oLen);
        }
        catch (CryptoException ce)
        {

            status(ce.ToString());
        }
        return rv;
    }

    private String performDecrypt(byte[] key, byte[] cipherText)
    {
        cipher.Init(false, new KeyParameter(key));

        byte[] rv = new byte[cipher.GetOutputSize(cipherText.Length)];

        int oLen = cipher.ProcessBytes(cipherText, 0, cipherText.Length, rv, 0);
        try
        {
            cipher.DoFinal(rv, oLen);
        }
        catch (CryptoException ce)
        {

        }
        return Encoding.ASCII.GetString(rv).Trim();
    }

    private int whichCipher()
    {
        return 1; // DES
    }

    private IBlockCipher getEngineInstance()
    {
        // returns a block cipher according to the current
        // state of the radio button lists. This is only
        // done prior to encryption.
        IBlockCipher rv = null;

        switch (whichCipher())
        {
            case 0:
                rv = new DesEngine();
                break;
            case 1:
                rv = new DesEdeEngine();
                break;
            case 2:
                rv = new IdeaEngine();
                break;
            case 3:
                rv = new RijndaelEngine();
                break;
            case 4:
                rv = new TwofishEngine();
                break;
            default:
                rv = new DesEngine();
                break;
        }
        return rv;
    }

    public void message(String s)
    {
        Console.WriteLine("M:" + s);
    }

    public void status(String s)
    {
        Console.WriteLine("S:" + s);
    }

    public byte[] convertStringToByteArray(String stringToConvert)
    {


        byte[] theByteArray = Encoding.ASCII.GetBytes(stringToConvert);

        Console.WriteLine(theByteArray.Length);
        return theByteArray;

    }

    public String convertByteArrayToString(byte[] byteArray)
    {

        //   byte[] byteArray = new byte[] {87, 79, 87, 46, 46, 46};

        String value = Encoding.ASCII.GetString(byteArray);

        Console.WriteLine(value);
        return value;
    }


    public byte[] hexToBytes(String hex)
    {
        //int NumberChars = hex.Length;
        //byte[] bytes = new byte[NumberChars / 2];
        //for (int i = 0; i < NumberChars; i += 2)
        //    bytes[i / 2] = Convert.ToByte(hex.ToUpper().Substring(i, 2), 16);
        //return bytes;
        string hexString = hex.ToUpper();

        return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
    }

    public string bytesToHex(byte[] ba)
    {
        string hex = BitConverter.ToString(ba);
        return hex.Replace("-", "");
    }

    public String stringToHex(String str)
    {
        char b;
        StringBuilder output = new StringBuilder();
        char[] chars = new char[str.Length];
        for (int i = 0; i < chars.Length; i++)
        {
            b = str[i];
            int value = Convert.ToInt32(b);
            // Convert the decimal value to a hexadecimal value in string form.
            string hexOutput = String.Format("{0:X}", value);
            output.Append(hexOutput);
        }
        return output.ToString().ToUpper();
    }

    public String doDESEncryption(String inputString)
    {
        DesEdeEngine engine = new DesEdeEngine();
        //BlockCipher engine = new DESedeEngine();
        BufferedBlockCipher myCipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine));

        byte[] key = this.hexToBytes(finalKey);//finalKey.getBytes();
        //byte[] input = inputString.GetBytes();
        byte[] input = Encoding.ASCII.GetBytes(inputString);

        myCipher.Init(true, new KeyParameter(key));

        byte[] cipherText = new byte[myCipher.GetOutputSize(input.Length)];

        int outputLen = myCipher.ProcessBytes(input, 0, input.Length, cipherText, 0);
        try
        {
            myCipher.DoFinal(cipherText, outputLen);
            //myCipher.doFinal(cipherText, outputLen);
        }
        catch (CryptoException ce)
        {
            //ce.printStackTrace();
            Console.WriteLine(ce.Message);
            return null;

            //System.exit(1);
        }

        byte[] cipherBytes = Hex.Encode(cipherText);
        return Encoding.ASCII.GetString(cipherBytes).ToUpper();

    }


}

#line default
#line hidden
