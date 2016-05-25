mkdir ~/builds
cd ~/builds

git clone https://github.com/Zebrai17/simple-build.git
cd simple-build/docker
docker build -t simple-build .
