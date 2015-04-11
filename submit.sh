rm submit/*.out
rm submit/*.zip
rm -rf submit/src
cp -rp src/ submit/src


cd submit

ls -t src/LSCoder.CodeJam/outputFiles/*.out | head -n 1 | xargs -I % cp % ./

rm -rf src/LSCoder.CodeJam/bin
rm -rf src/LSCoder.CodeJam/obj
rm -rf src/LSCoder.CodeJam/inputFiles
rm -rf src/LSCoder.CodeJam/outputFiles

zip -r src.zip src/
rm -rf src

# Exit from 'submit' folder
cd ..
