shader_type spatial;
render_mode cull_disabled, depth_draw_never;

uniform sampler2D depth_texture : source_color, hint_depth_texture;

uniform float radian_scale = 2.0;
uniform float color_bloom_alpha = 0.1;
uniform float color_bloom_range = 0.9;
uniform float color_bloom_noise_weight = 0.1;

uniform vec4 cubeColor = vec4(1,1,1,1);

#define INTERPOLANT 1

vec3 hash( vec3 p ) // replace this by something better
{
	p = vec3( dot(p,vec3(127.1,311.7, 74.7)),
			  dot(p,vec3(269.5,183.3,246.1)),
			  dot(p,vec3(113.5,271.9,124.6)));

	return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float noise( in vec3 p )
{
    vec3 i = floor( p );
    vec3 f = fract( p );

    #if INTERPOLANT==1
    // quintic interpolant
    vec3 u = f*f*f*(f*(f*6.0-15.0)+10.0);
    #else
    // cubic interpolant
    vec3 u = f*f*(3.0-2.0*f);
    #endif    

    return mix( mix( mix( dot( hash( i + vec3(0.0,0.0,0.0) ), f - vec3(0.0,0.0,0.0) ), 
                          dot( hash( i + vec3(1.0,0.0,0.0) ), f - vec3(1.0,0.0,0.0) ), u.x),
                     mix( dot( hash( i + vec3(0.0,1.0,0.0) ), f - vec3(0.0,1.0,0.0) ), 
                          dot( hash( i + vec3(1.0,1.0,0.0) ), f - vec3(1.0,1.0,0.0) ), u.x), u.y),
                mix( mix( dot( hash( i + vec3(0.0,0.0,1.0) ), f - vec3(0.0,0.0,1.0) ), 
                          dot( hash( i + vec3(1.0,0.0,1.0) ), f - vec3(1.0,0.0,1.0) ), u.x),
                     mix( dot( hash( i + vec3(0.0,1.0,1.0) ), f - vec3(0.0,1.0,1.0) ), 
                          dot( hash( i + vec3(1.0,1.0,1.0) ), f - vec3(1.0,1.0,1.0) ), u.x), u.y), u.z );
}


void vertex() {
  POSITION = vec4(VERTEX, 1.0);
}

float cube_color_bloom_value(vec2 screen_uv){
	float result = 0.0;
	
	vec2 center_uv = screen_uv * 2.0 - vec2(1.0);
	
	float rotate = atan(-center_uv.y/center_uv.x) + 0.5*PI;
	rotate = (sign(-center_uv.x)+1.0) * 0.5 * PI + rotate;

	float noise_value = noise(vec3(rotate)*radian_scale);
	
	float dist = distance(center_uv, vec2(0.0));
	dist = dist + noise_value * color_bloom_noise_weight;
	dist = pow(clamp(dist - color_bloom_range, 0.0, 1.0), 2) * color_bloom_alpha;
	
	return dist;
}


void fragment() {
	// Called for every pixel the material is visible on.
	float depth = texture(depth_texture, SCREEN_UV).x;
	
	ALBEDO = vec3(0,0,0);
	SPECULAR = 0.0;
	 
  	vec3 ndc = vec3(SCREEN_UV * 2.0 - 1.0, depth);
	vec4 pos = INV_PROJECTION_MATRIX * vec4(ndc, 1);
	float camera_depth = -(pos / pos.w).z;
	//EMISSION = vec3(-log((1.0-depth)) * 0.01);
	EMISSION = vec3(camera_depth * 0.1);
	
	EMISSION = cubeColor.xyz;
	ALPHA = cube_color_bloom_value(SCREEN_UV);
}

