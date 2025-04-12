using PiSpigot;

var N = 64;

//var OneOverEBase3 = "10022101122201".DigitSequence();
//Console.WriteLine(new BaseChange(3, 7, OneOverEBase3).Collate(N));

var rw = new RabinowitzWagon(N).Collate(N);
var gb = new Gibbons().Collate(N);
var fs = new FileSpigot("one-million.txt").Collate(N);

Console.WriteLine($"RW={rw}");
Console.WriteLine($"GB={gb}");
Console.WriteLine($"FS={fs}");

