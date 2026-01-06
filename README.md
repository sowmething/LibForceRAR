# LibForceRAR
RAR brute forcing library for windows written in C#

> ⚠️ **WARNING**
>
> This project is **for educational and research purposes only**.

> Unauthorized use of files that do not belong to you may be **unethical and illegal**.

> The developer is **not responsible** for any consequences arising from misuse.

## Setup
- Download or clone the project
- Extract if its zip file, and compile it
 OR
- You can download the compiled files in libforcerar/bin folder.

## Usage
Brute force file archive.rar, 30000 tries, write the result
```
using libforcerar;

string rarpass = ForceRar.Bruteforce("archive.rar", 30000);
Console.WriteLine(rarpass);
```

Brute force file archive.rar, 30000 tries, 500 threads, write the result
```
using libforcerar;

string rarpass = ForceRar.BruteforceThread("archive.rar", 30000, 500);
Console.WriteLine(rarpass);
```

Brute force file archive.rar, 30000 tries, custom unrar, write result
```
using libforcerar;

string rarpass = ForceRar.BruteforceCustomUnrar("archive.rar", 30000, "C:\\Program files\\Winrar\\Unrar.exe");
Console.WriteLine(rarpass);
```

Brute force file archive.rar, 30000 tries, 500 threads, custom unrar, write result
```
using libforcerar;

string rarpass = ForceRar.BruteforceCustomUnrarThread("archive.rar", 30000, "C:\\Program Files\\Winrar\\Unrar.exe", 500);
Console.WriteLine(rarpass);
```




