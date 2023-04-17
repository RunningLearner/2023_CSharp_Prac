using System.Text.RegularExpressions;

namespace SongNameSpace
{
    class Song
    {
        private readonly string _title;
        private readonly string _artistName;
        private readonly TimeSpan _length;

        public Song(string title, string artistName, int length)
        {
            _title = title;
            _artistName = artistName;
            _length = TimeSpan.FromSeconds(length);
        }

        public override string ToString()
        {
            bool isKorean = Regex.IsMatch(_artistName, @"^[가-힣]+$");

            // 맞춤된 문자열 생성
            string title = _title.PadRight(20);
            string artist = _artistName.PadRight(20);
            string korArtist = _artistName.PadRight(20 - _artistName.Length);
            string duration = _length.ToString(@"mm\:ss");

            // 탭으로 구분된 문자열 생성
            return $"Title: {title} Artist: {(isKorean ? korArtist : artist)} Duration: {duration} 초";
        }
    }
}