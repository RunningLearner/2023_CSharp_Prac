using SongNameSpace;

List<Song> playList = new()
{
    new Song("LILAC", "아이유", 231),
    new Song("Ice Cream", "블랙핑크", 178),
    new Song("How You Like That", "블랙핑크", 206),
    new Song("Rollin'", "브레이브걸스", 214),
    new Song("Gee", "소녀시대", 199),
    new Song("TT", "TWICE", 213),
    new Song("Dalla Dalla", "ITZY", 198),
    new Song("MAGO", "여자친구", 212),
    new Song("WANNABE", "ITZY", 188),
    new Song("Not Shy", "ITZY", 195)
};

foreach (var song in playList)
{
    Console.WriteLine(song);
}
