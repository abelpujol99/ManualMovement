# AA1: Manual Movement

## Team Description : Group B

Abel Pujol

abel.pujol@enti.cat

<img src='https://i.gyazo.com/e1f66a9470f2cb6892b108a7cba8d240.png' width='400'>



Adrià Pérez

adria.perez.garrofe@enti.cat

<img src='' width='400'>


## Ex2 Explanation:

We used spherical interpolation (slerp) over linear or normalised interpolation (lerp or lerpn) because it uniformly interpolates between two angles, which gives us a smoother arm movement.
The other options would've caused the animation to have inconsistent acceleration throughout the arc.
