using WindowMinSize;

Graphics.CreateWindow();

const string frag = """
                    #version 330
                    in vec2 Frag_UV;
                    in vec4 Frag_Color;
                    uniform sampler2D Texture;
                    layout (location = 0) out vec4 Out_Color;
                    void main()
                    {
                    	Out_Color = Frag_Color * texture(Texture, Frag_UV.st);
                    }
                    
                    """;

const string vert = """
                    #version 330
                    layout (location = 0) in vec2 Position;
                    layout (location = 1) in vec2 UV;
                    layout (location = 2) in vec4 Color;
                    uniform mat4 ProjMtx;
                    out vec2 Frag_UV;
                    out vec4 Frag_Color;
                    void main()
                    {
                    	Frag_UV = UV;
                    	Frag_Color = Color;
                    	gl_Position = ProjMtx * vec4(Position.xy,0,1);
                    }
                    
                    """;

uint shaderId = ShaderLoader.Load(vert, frag);
int projMtx = Graphics.Gl.GetUniformLocation(shaderId, "ProjMtx");
if (projMtx == -1)
	throw new InvalidOperationException("Could not find ProjMtx uniform location.");
int texture = Graphics.Gl.GetUniformLocation(shaderId, "Texture");
if (texture == -1)
	throw new InvalidOperationException("Could not find Texture uniform location.");
ImGuiController.Initialize(shaderId, projMtx, texture);

Game.Run();
