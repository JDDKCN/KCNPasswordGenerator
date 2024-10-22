namespace KCNPasswordGenerator.Utils
{
    internal class RandomUtil
    {
        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="intLength">随机字符串长度</param>
        /// <param name="booNumber">生成的字符串中是否包含数字</param>
        /// <param name="booSign">生成的字符串中是否包含符号</param>
        /// <param name="booSmallword">生成的字符串中是否包含小写字母</param>
        /// <param name="booBigword">生成的字符串中是否包含大写字母</param>
        /// <returns>返回生成的随机字符串</returns>
        public static string GetRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            char[] charPool = [];
            Random random = new Random();

            // 数字 (ASCII 48-57)
            if (booNumber)
                charPool = CombineCharArrays(charPool, GenerateCharRange(48, 57));

            // 小写字母 (ASCII 97-122)
            if (booSmallword)
                charPool = CombineCharArrays(charPool, GenerateCharRange(97, 122));

            // 大写字母 (ASCII 65-90)
            if (booBigword)
                charPool = CombineCharArrays(charPool, GenerateCharRange(65, 90));

            // 特殊符号 (ASCII 33-47, 58-64, 91-96, 123-126)
            if (booSign)
            {
                charPool = CombineCharArrays(charPool, GenerateCharRange(33, 47));
                charPool = CombineCharArrays(charPool, GenerateCharRange(58, 64));
                charPool = CombineCharArrays(charPool, GenerateCharRange(91, 96));
                charPool = CombineCharArrays(charPool, GenerateCharRange(123, 126));
            }

            if (charPool.Length == 0)
                return string.Empty;

            char[] result = new char[intLength];
            for (int i = 0; i < intLength; i++)
            {
                int index = random.Next(charPool.Length);
                result[i] = charPool[index];
            }

            return new string(result);
        }

        /// <summary>
        /// 合并两个char数组
        /// </summary>
        private static char[] CombineCharArrays(char[] array1, char[] array2)
        {
            char[] combined = new char[array1.Length + array2.Length];
            Array.Copy(array1, 0, combined, 0, array1.Length);
            Array.Copy(array2, 0, combined, array1.Length, array2.Length);
            return combined;
        }

        /// <summary>
        /// 生成ASCII集合范围内的字符
        /// </summary>
        private static char[] GenerateCharRange(int startAscii, int endAscii)
        {
            int length = endAscii - startAscii + 1;
            char[] range = new char[length];
            for (int i = 0; i < length; i++)
            {
                range[i] = (char)(startAscii + i);
            }
            return range;
        }
    }
}
