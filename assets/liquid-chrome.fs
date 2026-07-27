/*{
  "DESCRIPTION": "A flowing mercury-like ripple across the frame with a soft metallic sheen — a living, liquid-chrome shimmer.",
  "CATEGORIES": ["Guillotine", "Distortion"],
  "INPUTS": [
    { "NAME": "inputImage", "TYPE": "image" },
    { "NAME": "amount", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 1.0 },
    { "NAME": "speed", "TYPE": "float", "DEFAULT": 0.6, "MIN": 0.1, "MAX": 2.0 }
  ]
}*/

void main() {
  vec2 uv = isf_FragNormCoord;

  // Two out-of-phase sine waves warp the sampling coordinate in x and y, so the whole frame ripples
  // like liquid metal rather than shifting as one rigid block.
  float wobbleX = sin(uv.y * 12.0 + TIME * speed) * 0.01 * amount;
  float wobbleY = cos(uv.x * 10.0 - TIME * speed * 0.8) * 0.01 * amount;
  vec2 warped = uv + vec2(wobbleX, wobbleY);

  vec4 c = IMG_NORM_PIXEL(inputImage, warped);

  // A soft metallic sheen: push highlights brighter and slightly desaturated, chrome-like.
  float luma = dot(c.rgb, vec3(0.299, 0.587, 0.114));
  vec3 sheen = mix(c.rgb, vec3(luma) * 1.3, 0.15 * amount);

  gl_FragColor = vec4(sheen, c.a);
}
