#!/bin/bash
cd /usr/local
mkdir paket
cd ./paket

nuget install paket 
mv */*/* /usr/lib/mono/4.5

echo 'alias pi=mono /usr/lib/mono/4.5/paket.exe init' >> /etc/