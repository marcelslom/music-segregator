# Music Segregator

Music Segregator is a C# program that organizes MP3 files based on their ID3 tags (artist and album) into folders.

## Features

- Sorts MP3 files into folders based on ID3 tags.
- Supports customization of destination folder and filename format.
- Option to move or copy files to the destination folder.
- Generates informational logs.
- Recursive search for files in subdirectories.
- Automatically modifies destination filenames to avoid duplicates by appending `(1)`, `(2)`, and so on, if necessary.

## Usage

To use Music Segregator, follow these steps:

1. Download the latest release from the [Releases](https://github.com/marcelslom/music-segregator/releases) page.
2. Unzip the downloaded archive.
3. Open a terminal window and navigate to the folder where you extracted the files.
4. Execute Program:
    - Run `.\musicseg` in the terminal if you're using Windows.
    - Alternatively, you can add the folder containing the executable (`musicseg.exe`) to your system's PATH to run it from any location in the terminal.

### Arguments

```
musicseg [options] [source]
```
Positional arguments:
- `source` The path to the source folder containing MP3 files. If not provided, the current directory will be used by default.

Options:
- `-d, --destination <destination>` The destination folder where segregated music files will be stored. If not provided, will be set to `{current_directory}/music_segregated`.
- `-m, --move-files` Option that specifies whether files should be moved to the destination folder. By default, files will be copied.
- `-f, --filename-format <filename-format>` The format for renaming processed files. If provided, this format will be used for renaming. If not provided, the filename will remain unchanged. Do NOT provide file extension in this parameter, as program will use the original one. E. g. `{Track} - {Title}`.
- `-l, --log` Option that enables writing informational logs to a file.
- `-r, --recursive` Option that specifies whether the program should search for files in subdirectories. Default behavior is to search in the top directory only.
- `-h, --help` Show help message and exit.

### Parameters available for file rename
- AlbumArtists
- Performers
- Composers
- Genres
- Title
- Album
- Year
- Track
- TrackCount
- Disc
- DiscCount
- InitialKey
- BeatsPerMinute

### Example usage:

The simplest usage is just executing
```
musicseg
```
This command copies MP3 files from current directory (top only, without subfolders) into folders structure created in the `music_segregated` subfolder.

Here is example that gets the most out of the **Music Segregator**.

```
musicseg -d "C:\Music\Sorted" -f "{Artist} - {Title}" -l -m -r "C:\Music\Unsorted"
```
This example command organizes MP3 files from the `C:\Music\Unsorted` directory. It sorts them into folders within `C:\Music\Sorted`, and renames files using the format `{Artist} - {Title}`. Additionally, it enables logging (`-l`) and moves (`-m`) the files instead of copying them. The sorting process includes subdirectories (`-r`).

## Destination structure

The destination structure for sorted MP3 files is based on the ID3 tags of each file. It uses the album artist's name as the primary directory. If the album artist's name is unavailable, it falls back to the track performer's name or a default name specified in the program. If the album name is available, it creates a subdirectory within the artist's directory.

## Logs

Logs are stored in `{source_directory}/logs` folder in 2 files:
- `errors.${shortdate}.log` contains information about errors that occured during program execution.
- `info.${shortdate}.log` contains information about each file that was processed by program (information is appended only if `-l` flag was set in CLI).