msbuild -p:Configuration=Release
rm BoundsMaker.dll
cd bin/Release/net48
declare -a copy=("BoundsMaker.dll")
for i in "${copy[@]}"
do
    cp "$i" ../../../
done
cd ../../../