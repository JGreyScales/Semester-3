taskkill /F /IM VirtualBox.exe
taskkill /F /IM VBoxSVC.exe
taskkill /F /IM VBoxHeadless.exe
taskkill /F /IM VBoxNetAdp.exe
taskkill /F /IM VBoxNetDhcp.exe


net stop vboxdrv
net stop vboxnetadp
net stop vboxnetdhcp


net start vboxdrv
net start vboxnetadp
net start vboxnetdhcp