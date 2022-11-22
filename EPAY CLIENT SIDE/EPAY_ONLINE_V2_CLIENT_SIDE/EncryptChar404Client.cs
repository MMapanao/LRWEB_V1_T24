using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24
{
	public class EncryptChar404Client
	{
		string Key1 = "A|B|C|D|E|F|G|H|J|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|1|2|3|4|5|6|7|8|9|0"
		+ ("|A|B|C|D|E|F|G|H|J|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z").ToLower();
		string Key1Value = "p9|l8|m7|n6|j5|i4|u3|h2|b1|v0|g1|y2|t3|f4|c5|x6|d7|r8|e9|w8|s7|z6|a5|q4|3w|2e|1r|0t|1y|2u|3i|4o|5p|6m|7n|8b|9v" +
			("|g1|y2|t3|f4|c5|x6|d7|r8|e9|w8|s7|z6|a5|q4|3w|2e|1r|0t|1y|2u|3i|4o|5p|6m|7n|8b|9v").ToUpper();

		public string OneWayEncode404(string SecurityKey, string EncodeText)
		{
			string m = EncryptBCrypt(EncodeText);
			List<string> mList = new List<string>();
			for (int x = 0; x < m.Length; x++)
			{
				mList.Add(m.Substring(x, 1));
			}
			string EncryptionResult = EncryptionFirstLayer(mList, SecurityKey);
			List<string> fList = new List<string>();
			for (int x = 0; x < EncryptionResult.Length; x++)
			{
				fList.Add(EncryptionResult.Substring(x, 1));
			}
			string finalResult = EncryptionFinalLayer(fList);
			return finalResult;
		}

		public string TwoWayEncode404(string SecurityKey, string EncodeText)
		{

			string m = EncodeText;
			List<string> mList = new List<string>();
			for (int x = 0; x < m.Length; x++)
			{
				mList.Add(m.Substring(x, 1));
			}
			string EncryptionResult = EncryptionFirstLayer(mList,SecurityKey);
			List<string> fList = new List<string>();
			for (int x = 0; x < EncryptionResult.Length; x++)
			{
				fList.Add(EncryptionResult.Substring(x, 1));
			} 
			string finalResult = EncryptionFinalLayer(fList);
			return finalResult;
		}

		public string TwoWayDecode404(string SecurityKey, string EncodeText)
		{
			List<string> Dec_fList = new List<string>();
			for (int x = 0; x < EncodeText.Length; x++)
			{
				Dec_fList.Add(EncodeText.Substring(x, 1));
			}
			string Decrypt_First = DecryptFirst(Dec_fList);


			List<string> Dec_mList = new List<string>();
			for (int x = 0; x < Decrypt_First.Length; x += 5)
			{
				Dec_mList.Add(Decrypt_First.Substring(x, 5));
			}
			string Decrypt_Final = DecriptFinal(Dec_mList);

			return (Decrypt_Final);
		}

		public string EncryptionFinalLayer(List<string> fList)
		{

			string result = string.Empty;
			var arrKey1 = Key1.Split('|');
			foreach (var kv in fList)
			{
				int indcount = 0;
				int found = 0;
				do
				{
					if (kv == arrKey1[indcount])
					{
						result += arrKey1[getLocationArray(indcount, arrKey1.Length - 1)];
						found++;
						break;
					}
					indcount++;
				} while (indcount < arrKey1.Length);
				{
					if (found == 0)
					{
						result += kv;
					}
				}
			}
			return result;
		}
		public int getLocationArray(int x, int arrLength)
		{
			if ((x - arrLength) < 0)
			{
				return (x - arrLength) * (-1);
			}
			else
			{
				return (x - arrLength);
			}
		}
		public string EncryptionFirstLayer(List<string> mList,string SecurityKey)
		{ 
			var arr_Key1 = Key1.Split('|');
			var arr_Key1Value = Key1Value.Split('|');
			string res = string.Empty;
			foreach (var ex in mList)
			{
				int xKey = 0;
				int resFound = 0;
				int resnum = 0;
				do
				{
					if (arr_Key1[xKey] == ex)
					{
						resnum = RandomNumber(0, arr_Key1.Length);
						if (resnum > 9)
						{
							res += resnum + arr_Key1Value[xKey].Substring(0, 1) +
								GetSecurityKey(arr_Key1.Length, SecurityKey, resnum) +
								arr_Key1Value[xKey].Substring(1, 1);
						}
						else
						{
							res += "0" + resnum + arr_Key1Value[xKey].Substring(0, 1) +
								GetSecurityKey(arr_Key1.Length, SecurityKey, resnum) +
								arr_Key1Value[xKey].Substring(1, 1);
						}
						resFound++;
						break;
					}
					xKey++;
				} while (xKey < arr_Key1.Length);
				{
					if (resFound == 0)
					{
						if (resnum > 9)
						{
							res += resnum + ex +
								GetSecurityKey(arr_Key1.Length, SecurityKey, resnum) +
								ex;
						}
						else
						{
							res += "0" + resnum + ex +
								GetSecurityKey(arr_Key1.Length, SecurityKey, resnum) +
								ex;
						}
					}
				}
			}
			return res;
		}
		public string GetSecurityKey(int paramSize, string securitykey, int location)
		{
			string final_key = string.Empty;
			do
			{
				final_key = final_key + securitykey;
			} while (final_key.Length <= paramSize);
			{
				return final_key.Substring(location, 1);
			}
		}
		public int RandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max);
		}

		public string DecryptFirst(List<string> Dec_fList)
		{
			string result = string.Empty;
			var arrKey1 = Key1.Split('|');
			foreach (var kv in Dec_fList)
			{
				int indcount = 0;
				int found = 0;
				do
				{
					if (kv == arrKey1[indcount])
					{
						result += arrKey1[getLocationArray(indcount, arrKey1.Length - 1)];
						found++;
						break;
					}
					indcount++;
				} while (indcount < arrKey1.Length);
				{
					if (found == 0)
					{
						result += kv;
					}
				}
			}
			return result;
		}
		public string DecriptFinal(List<string> Dec_mList)
		{
			string result = string.Empty;
			var arrKey1 = Key1.Split('|');
			var arrKeyValue = Key1Value.Split('|');
			foreach (var dec in Dec_mList)
			{
				string partial_result = dec.Substring(2, 1) + dec.Substring(4, 1);
				int ind_count = 0;
				int found = 0;
				int count = 0;
				do
				{
					if (arrKeyValue[ind_count] == partial_result)
					{
						result += arrKey1[ind_count];
						found++;
						break;
					}
					ind_count++;
				} while (arrKeyValue.Length > ind_count);
				{
					if (found == 0)
					{
						result += partial_result.Substring(0, 1);
					}
				}
			}


			return result;
		}

		public  string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
		public  string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		public  string EncryptBCrypt(string key)
		{
			key = Base64Encode(key);
			MD5 md5 = new MD5CryptoServiceProvider();
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
			byte[] result = md5.Hash;
			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				strBuilder.Append(result[i].ToString("x2"));
			}
			key = strBuilder.ToString();
			return key;
		}
		public int getDecryptLocationArray(int x, int arrLength)
		{
			if ((x - arrLength) < 0)
			{
				return ((x - arrLength)) * (-1);
			}
			else
			{
				return (x - arrLength);
			}
		}

 
	}
}