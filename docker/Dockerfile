FROM ubuntu:14.04
MAINTAINER rick.doucet@implementingeventsourcingwithfsharp.com

ADD mono-fsharp-setup.sh /usr/local/scripts/
ADD paket-setup.sh /usr/local/scripts/
ADD nuget-setup.sh /usr/local/scripts/
ADD nuget-setup.sh /usr/local/scripts/
ADD fake-pre-reqs-setup.sh /usr/local/scripts/

RUN apt-get update && apt-get upgrade -y
RUN cd /usr/local/scripts/ && ./mono-fsharp-setup.sh && ./paket-setup.sh && ./nuget-setup.sh && ./fake-pre-reqs-setup.sh
