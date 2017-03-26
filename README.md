# OpenTKVSyncDebug
Template / sandbox for investigating [OpenTK issue #467](https://github.com/opentk/opentk/issues/467)

## What it does

It's a simple render loop with some simulated busy work that draws a row of moving squares at constant speed and measures frame times. Whenever frame time exceeds 16.66ms by a significant amount, the squares flash in red to highlight the detected frame time spike.

This sample uses the [2017-03-23 develop branch version](https://github.com/opentk/opentk/tree/47ef73fae7aeff68e43c512309be480602fc8f5b) of OpenTK, which is included in the repo for convenience.

## How the problem looks like

I'm not sure whether the problem is present on all systems. [This is how it looks like](https://gfycat.com/HighWaterloggedAntipodesgreenparakeet) on my machine. As you can see, there are visible frame spikes occurring at irregular intervals.

## How to test on your machine

You can download the [pre-built binaries](https://github.com/ilexp/OpenTKVSyncDebug/raw/master/Build.zip) or clone the git repo and build it yourself.
