# LibForceRAR
RAR brute forcing library for windows written in C#

> ⚠️ **WARNING**
>
> This project is **for educational and research purposes only**.

> Unauthorized use of files that do not belong to you may be **unethical and illegal**.

> The developer is **not responsible** for any consequences arising from misuse.

LibForceRAR does not implement any cryptographic algorithm itself.
It performs password verification by invoking an external UnRAR executable.

The library attempts to extract the archive using a candidate password and
determines success based on the UnRAR process exit code.

Supported RAR formats and encryption methods depend entirely on the installed
UnRAR version.

## Brute-force strategy

- Passwords are generated as incremental numeric values
- Range: 0 → maxNumber
- Charset: digits only (0–9)
- No dictionary or combinatorial brute-force is implemented

## Multithreading behavior

- Each password attempt spawns a separate UnRAR process
- Excessive thread counts may significantly degrade performance
- Recommended thread count: <= number of logical CPU cores
- Performance is primarily disk I/O bound

## Compatibility

| RAR Type | Status |
|--------|--------|
| RAR4 (AES-128) | Supported via UnRAR |
| RAR5 (AES-256) | Depends on UnRAR version |
| Solid archives | Supported, but significantly slower |
| Multipart archives | Supported if UnRAR supports them |

(Doesnt contain all of the supported algorithms. Status might not be %100 accurate.)

## Limitations

- No cryptographic cracking is performed
- Numeric passwords only
- No checksum or header-level verification
- False negatives may VERY VERY RARELY occur depending on archive structure

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
UnRAR exit code 0 is interpreted as successful extraction.
All other exit codes are treated as failed password attempts.






