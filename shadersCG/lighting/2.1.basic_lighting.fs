#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 lightColor;
uniform vec3 objectColor;

// Variables to controle light
uniform float ambientOption;
uniform float diffuseOption;
uniform float specularOption;
uniform float ambientStrength;
uniform float specularStrength;

void main()
{
    // ambient -> If ambientOption == 1, then calculate ambient, else ambient = 0.
    vec3 ambient = vec3(0.0, 0.0, 0.0);
    if(ambientOption > 0.5){
        ambient = ambientStrength * lightColor;
    }

    // diffuse -> If diffuseOption == 1, then calculate diffuse, else diffuse = 0.
    vec3 diffuse = vec3(0.0, 0.0, 0.0);
    vec3 lightDir = normalize(lightPos - FragPos);
    vec3 norm = normalize(Normal);
    float diff = max(dot(norm, lightDir), 0.0);
    if(diffuseOption > 0.5){
        diffuse = diff * lightColor;
    }

    // specular -> If specularOption == 1, then calculate specular, else specular = 0.
    vec3 specular = vec3(0.0, 0.0, 0.0);
    vec3 viewDir = normalize(-FragPos); // the viewer is always at (0,0,0) in view-space, so viewDir is (0,0,0) - Position => -Position
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    if(specularOption > 0.5){
        specular = specularStrength * spec * lightColor; 
    }
            
    vec3 result = (ambient + diffuse+ specular) * objectColor;
    FragColor = vec4(result, 1.0);
} 

