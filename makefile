MDTOOL="/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"
MTOUCH=/Developer/MonoTouch/usr/bin/mtouch
TOUCH_SERVER=./Touch.Unit/Touch.Server/bin/Debug/Touch.Server.exe

all: build-simulator

run run-test: run-simulator

build-server:
	cd Touch.Unit/Touch.Server && xbuild

build-simulator:
	$(MDTOOL) -v build -t:Build "-c:Debug|iPhoneSimulator" -p:Tests Rocks.sln

run-simulator: build-simulator build-server
	rm -f sim-results.log
	mono --debug $(TOUCH_SERVER) --launchsim Tests/bin/iPhoneSimulator/Debug/Tests.app -autoexit -logfile=sim-results.log
	cat sim-results.log