namespace AsyncReader;

// test 파일 제작용 클래스
public static class TextWriter
{
    public static void WriteFile(string fileName)
    {
        // 파일을 쓰기 모드로 엽니다.
        using StreamWriter writer = new(fileName, false);
        // 파일에 내용을 씁니다.
        writer.WriteLine("//async");

        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine("//{0}번째 줄입니다.", i);
        }

        writer.WriteLine("//await");
    }
}