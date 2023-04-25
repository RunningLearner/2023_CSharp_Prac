namespace TextfileServices;

public interface ITextFileService
{
    void Initialize(string name);
    void Excute(string line);
    void Terminate();
}