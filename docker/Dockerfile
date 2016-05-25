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

#install docker copied from https://github.com/jpetazzo/dind/blob/master/Dockerfile
# Let's start with some basic stuff.
RUN apt-get update -qq && apt-get install -qqy \
    apt-transport-https \
    ca-certificates \
    curl \
    lxc \
    iptables
    
# Install Docker from Docker Inc. repositories.
RUN cd / && curl -sSL https://get.docker.com/ | sh

# Install the magic wrapper.
ADD ./wrapdocker /usr/local/bin/wrapdocker
ADD ./build-script.sh /usr/local/bin/build-script.sh

RUN chmod +x /usr/local/bin/wrapdocker
RUN chmod +x /usr/local/bin/build-script.sh

# Define additional metadata for our image.
VOLUME /var/lib/docker
CMD ["wrapdocker"]
