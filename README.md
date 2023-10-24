# AA1: Manual Movement

## Team Description : Group B

Abel Pujol

abel.pujol@enti.cat

<img src='https://i.gyazo.com/e1f66a9470f2cb6892b108a7cba8d240.png' width='400'>



Adrià Pérez

adria.perez.garrofe@enti.cat

<img src='https://cdn.discordapp.com/attachments/913391373762326570/1166447312797716652/PXL_20230425_234301451.jpg?ex=654a85b6&is=653810b6&hm=42cc130ec6263409da57a351eac28d2c6b3ebf534eb2179afec0d1f7005627f3&' width='400'>


## Ex2 Explanation:

We used spherical interpolation (slerp) over linear or normalised interpolation (lerp or lerpn) because it uniformly interpolates between two angles, which gives us a smoother arm movement.
The other options would've caused the animation to have inconsistent acceleration throughout the arc.
