- Spiral field: `(y-x)/5 i + (-x-y)/5 j`
  ![spiral](./spiral.jpg)
***
- An eyes like shape: `sin(y) i + x/3 j`
  ![eyes](./eyes.jpg)
***
- Demonstrating transition from `x/4 i + y/4 j`(outward lines) to `y/4 i + -x/4 j`(nested circles)

![outward_lines](./outward_lines.jpg)
![outward_to_circles](./outward_to_circles.jpg)
![nested_circles](./nested_circles.jpg)
***
- Inverse Square Field: `x/(r*r) i + y/(r*r) j`

![Inverse_Square](./Inverse_Square.jpg)
## Shape Field

This is a field equation that results in polygon like shapes(and moves as a whole with time):
<br/>
`cos(sin(time * a) + time) * scale i + sin(sin(time * a) + time) * scale j` where `a = number of vertices for the shape`
For better visualization, I increased particle lifetime, count and increased space between generator 
- `a=3`
![ShapeField_3](./ShapeField_3.jpg)
***
- `a=4`
![ShapeField_4](./ShapeField_4.jpg)
***
- `a=5`
![ShapeField_5](./ShapeField_5.jpg)
***
- `a=0`
![ShapeField_0](./ShapeField_0.jpg)
  As one would expect,  `cos(time) i + sin(time) j` creates circles.
  <br/>
  ***
When `a` is in the middle of two numbers, it looks like it merges the `a=floor(a)` shape with `a=ceil(a)` shape

- `a=3.5`
![ShapeField_3_5](./ShapeField_3_5.jpg)
It looks like it merges the shapes with `a=3` and `a=4`, at the same time its neither distinctly 3 pointed or 4 pointed.
<br/>
***
- `a=5.5`
![ShapeField_5_5](./ShapeField_5_5.jpg)

***

- `a=0.5`
![WeirdLeaves](./WeirdLeaves.jpg)
This creates a leaves/flower like pattern, but is not stable and looks different with increased lifetime

---
These shapes are all I can show with images but watching their shapes clarify, transition from one to another shape is fun and experimenting with the equation always creates some unexpected shape.