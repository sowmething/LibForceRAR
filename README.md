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
Brute force file archive.rar, number until 30000 
```
using libforcerar;

string rarpass = ForceRar.Bruteforce("archive.rar", 30000);```

