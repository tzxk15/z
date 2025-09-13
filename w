local Lib = {}
if game.CoreGui:FindFirstChild("LibHorizontal") then
    game.CoreGui:FindFirstChild("LibHorizontal"):Destroy()
end

local ScreenGui = Instance.new("ScreenGui", game.CoreGui)
ScreenGui.Name = "LibHorizontal"

-- toggle de visible con F6
local Usp = game:GetService("UserInputService")
local visible = true
local Usable = true
if _G.HideKeybind == nil then
    _G.HideKeybind = Enum.KeyCode.F6
end
Usp.InputBegan:Connect(function(key)
    if key.KeyCode == _G.HideKeybind and Usable then
        Usable = false
        for _,v in pairs(ScreenGui:GetChildren()) do
            spawn(function()
                if visible then
                    v:TweenPosition(UDim2.new(v.Position.X.Scale,v.Position.X.Offset,0,-500),
                        Enum.EasingDirection.In,Enum.EasingStyle.Sine,0.5,true)
                    wait(0.4)
                    v.Visible = false
                else
                    v.Visible = true
                    v:TweenPosition(UDim2.new(v.Position.X.Scale,v.Position.X.Offset,0,50),
                        Enum.EasingDirection.In,Enum.EasingStyle.Sine,0.5,true)
                end
            end)
        end
        wait(0.05)
        Usable = true
        visible = not visible
    end
end)

function Lib:CreatePanel(Name)
    local Panel = {}
    Panel.flags = {}

    -- Main window (robusta, más grande)
    local Main = Instance.new("Frame")
    Main.Name = "Main"
    Main.Parent = ScreenGui
    Main.BackgroundColor3 = Color3.fromRGB(25,25,25)
    Main.BorderSizePixel = 0
    Main.Position = UDim2.new(0, 50, 0, 50)
    Main.Size = UDim2.new(0, 700, 0, 250)

    local Corner = Instance.new("UICorner", Main)
    Corner.CornerRadius = UDim.new(0, 8)

    -- Barra de título
    local TitleBar = Instance.new("Frame", Main)
    TitleBar.BackgroundColor3 = Color3.fromRGB(15,15,15)
    TitleBar.Size = UDim2.new(1,0,0,25)

    local Title = Instance.new("TextLabel", TitleBar)
    Title.Text = Name
    Title.Size = UDim2.new(1,0,1,0)
    Title.BackgroundTransparency = 1
    Title.TextColor3 = Color3.new(1,1,1)
    Title.Font = Enum.Font.Roboto
    Title.TextSize = 14

    -- Drag system
    local dragging, dragInput, dragStart, startPos
    local function update(input)
        local delta = input.Position - dragStart
        Main.Position = UDim2.new(startPos.X.Scale, startPos.X.Offset + delta.X,
                                  startPos.Y.Scale, startPos.Y.Offset + delta.Y)
    end
    TitleBar.InputBegan:Connect(function(input)
        if input.UserInputType == Enum.UserInputType.MouseButton1 or input.UserInputType == Enum.UserInputType.Touch then
            dragging = true
            dragStart = input.Position
            startPos = Main.Position
            input.Changed:Connect(function()
                if input.UserInputState == Enum.UserInputState.End then
                    dragging = false
                end
            end)
        end
    end)
    TitleBar.InputChanged:Connect(function(input)
        if input.UserInputType == Enum.UserInputType.MouseMovement and dragging then
            update(input)
        end
    end)

    -- Contenedor con scroll horizontal
    local Scrolling = Instance.new("ScrollingFrame", Main)
    Scrolling.Position = UDim2.new(0,0,0,25)
    Scrolling.Size = UDim2.new(1,0,1,-25)
    Scrolling.BackgroundTransparency = 1
    Scrolling.ScrollBarThickness = 8
    Scrolling.ScrollingDirection = Enum.ScrollingDirection.X
    Scrolling.CanvasSize = UDim2.new(0,0,0,0)

    local Layout = Instance.new("UIListLayout", Scrolling)
    Layout.FillDirection = Enum.FillDirection.Horizontal
    Layout.SortOrder = Enum.SortOrder.LayoutOrder
    Layout.Padding = UDim.new(0,10)

    Layout:GetPropertyChangedSignal("AbsoluteContentSize"):Connect(function()
        Scrolling.CanvasSize = UDim2.new(0, Layout.AbsoluteContentSize.X+10,0,0)
    end)

    --------------------------------------------------------------------
    -- Aquí reusamos las funciones de tu UI original pero adaptadas
    -- para meter cada elemento dentro de Scrolling horizontal
    --------------------------------------------------------------------

    function Panel:Button(name, callback)
        local Btn = Instance.new("TextButton", Scrolling)
        Btn.Size = UDim2.new(0,120,0,50)
        Btn.Text = name
        Btn.Font = Enum.Font.Roboto
        Btn.TextSize = 14
        Btn.BackgroundColor3 = Color3.fromRGB(40,40,40)
        Btn.TextColor3 = Color3.new(1,1,1)
        Instance.new("UICorner", Btn).CornerRadius = UDim.new(0,6)
        Btn.MouseButton1Click:Connect(callback or function()end)
    end

    -- Puedes replicar Toggle, Slider, Dropdown, Bind, Box, Search, Section, Label
    -- siguiendo exactamente la misma lógica que en tu UI.txt,
    -- SOLO que ahora el "parent" debe ser `Scrolling` y cada uno tendrá
    -- Width fijo y Height fijo (ej. 150x60) para encajar horizontalmente.
    -- Los flags se manejan igual: location[flag] = valor

    return Panel
end

return Lib
