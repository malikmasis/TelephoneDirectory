docker run --name redis -p:6379:6379 -d redis 

start maingo.bat

Start-Sleep -Seconds 5

cd ./
start subgo.bat

Start-Sleep -Seconds 5

cd ./
tye run --watch