run studio command prompt to work with dnvm
dnvm = .NET Version Manager v1.0.0-beta5-10384
You’ll also want to make sure you get the latest version of the runtime by opening a console and typing:
dnvm upgrade - does something to download the latest version
gets installed here C:\Users\atrifono\.dnx\runtimes\dnx-clr-win-x86.1.0.0-rc1-update1\bin
dnx = Microsoft .NET Execution environment Clr-x86-1.0.0-rc1-16231
we want to create an initial migration by running
dnx ef migrations add Initial
"ef" is defined in project.json
Looks like the startup is needed to bootstrap the dnx services anyway. It will not be used when dll is referenced from another project.
dnx ef migrations add Initial <- needs to be executed from project folder, not solution folder
dnu restore <- restores packages
run dnmv upgrade to add dnx to path
dnvm use 1.0.0-rc1-update1
dnx ef migrations add Initial
dnx ef database update