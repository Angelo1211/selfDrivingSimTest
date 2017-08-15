# LiDAR Scanner modeling in Unity3D

This project aims to model a liDAR scanner in a small test city environment using Unity3D as renderer and physics engine. It first reads geometry data from the environment using collision raycasting and returns an impact point array for each  scan. The scan rate is currently limited by Unity's physics engine refresh rate and the maximum mesh size of 65k vertices. Before running the simulation the rotation rate of the scanner can be set which allows for a trade off between angular resolution and refresh rate. The results are rendered using a vertex shader that draws each collision point as a vertex and colors it based on the distance from origin.

The default Unity vehicle controller has been attached to the car and can be used to model the effect of a moving vehicle and the sensor, but will cause significant FPS drops. I recommend you reduce the physics deltaTime to a value around (0.04-0.01) to get above 30fps.

## Getting Started

This repo includes the project as-is. Including many default assets that were not used in the final version. Download and copy all assets to a new Unity project and make sure to modify the fixedDeltaTime value in the editor to get a more defined point cloud. V-sync should be disabled for better performance.

### Prerequisites

What things you'll need to install the software:

```
Unity3D (Version 2017.1.0f3 (64-bit) was used, not checked for previous versions)
```

### Sample Images & GIF's

I also included some images for reference but since vertices are represented as single pixels image compression has made it hard to see them. Download the images for better results or run the project.

![Alt text](/images/MovingObject.gif "Visual representation of point cloud updating when an object enters it's FOV")

![Alt text](/images/lidarTest2.PNG "Showing Editor view and lidarScan view")

![Alt text](/images/liDARTest1.PNG "Test at 5hz")


## Authors

* **Angel Ortiz** - *Project Lead* - [GitHUB](https://github.com/Angelo1211)

## References

* http://developer.download.nvidia.com/CgTutorial/cg_tutorial_appendix_e.html
* http://velodynelidar.com/hdl-64e.html
* http://catlikecoding.com/unity/tutorials/
