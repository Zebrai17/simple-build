FROM ubuntu:14.04
MAINTAINER rick.doucet@implementingeventsourcingwithfsharp.com


#apply latest updates for packages installed
RUN apt-get update && apt-get upgrade -y

#install mono version 4 and fsharp
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
RUN echo "deb http://download.mono-project.com/repo/debian wheezy-apache24-compat main" | sudo tee -a /etc/apt/sources.list.d/mono-xamarin.list
RUN apt-get update && apt-get install -y mono-complete && apt-get install -y fsharp

#install nuget & paket
RUN apt-get install -y nuget
RUN mkdir ~/paket


RUN cd ~/paket && nuget install paket && mv ~/paket/*/*/* /usr/lib/mono/4.5
RUN rm -R ~/paket

#create pi alias which maps tp 'paket init .'
RUN echo "alias pi='mono /usr/lib/mono/4.5/paket.exe init'" >> /etc/bash.bashrc

#install missing dependency for FAKE
RUN mkdir ~/Automation.Managment
RUN cd ~/Automation.Managment && nuget install System.Management.Automation
RUN mv ~/Automation.Managment/*/*/net45/* /usr/lib/mono/4.5

#git
RUN apt-get install -y git 

RUN apt-get install -y apt-transport-https ca-certificates
RUN apt-key adv --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys 58118E89F3A912897C070ADBF76221572C52609D
RUN echo 'deb https://apt.dockerproject.org/repo ubuntu-trusty main' > /etc/apt/sources.list.d/docker.list

RUN apt-get update

RUN apt-get install -y docker-engine

ADD ./build.exec.exe /usr/lib/mono/4.5/build.exec.exe

