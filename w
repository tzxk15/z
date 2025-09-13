local Lib = {}
if game.CoreGui:FindFirstChild("LibHorizontal") then
    game.CoreGui:FindFirstChild("LibHorizontal"):Destroy()
end

local ScreenGui = Instance.new("ScreenGui", game.CoreGui)
ScreenGui.Name = "LibHorizontal"

function Lib:CreatePanel(Name)
    local Panel = {}

    -- Contenedor principal
    local Main = Instance.new("Frame")
    Main.Name = "Main"
    Main.Parent = ScreenGui
    Main.BackgroundColor3 = Color3.fromRGB(20, 20, 20)
    Main.BorderSizePixel = 0
    Main.Position = UDim2.new(0, 50, 0, 50)
    Main.Size = UDim2.new(0, 500, 0, 70)

    -- Título arriba
    local Title = Instance.new("TextLabel")
    Title.Parent = Main
    Title.Size = UDim2.new(1, 0, 0, 20)
    Title.BackgroundTransparency = 1
    Title.Text = Name
    Title.Font = Enum.Font.Roboto
    Title.TextSize = 14
    Title.TextColor3 = Color3.fromRGB(255, 255, 255)
    Title.TextXAlignment = Enum.TextXAlignment.Center

    -- Scrolling para horizontal
    local Scrolling = Instance.new("ScrollingFrame")
    Scrolling.Parent = Main
    Scrolling.Position = UDim2.new(0, 0, 0, 20)
    Scrolling.Size = UDim2.new(1, 0, 1, -20)
    Scrolling.CanvasSize = UDim2.new(0,0,0,0)
    Scrolling.BackgroundTransparency = 1
    Scrolling.ScrollBarThickness = 6
    Scrolling.ScrollingDirection = Enum.ScrollingDirection.X

    local Layout = Instance.new("UIListLayout")
    Layout.Parent = Scrolling
    Layout.FillDirection = Enum.FillDirection.Horizontal
    Layout.SortOrder = Enum.SortOrder.LayoutOrder
    Layout.Padding = UDim.new(0, 5)

    local function UpdateCanvas()
        Scrolling.CanvasSize = UDim2.new(0, Layout.AbsoluteContentSize.X, 0, 0)
    end
    Layout:GetPropertyChangedSignal("AbsoluteContentSize"):Connect(UpdateCanvas)

    function Panel:Button(Text, Callback)
        local Btn = Instance.new("TextButton")
        Btn.Parent = Scrolling
        Btn.Size = UDim2.new(0, 100, 0, 40)
        Btn.BackgroundColor3 = Color3.fromRGB(40, 40, 40)
        Btn.Text = Text
        Btn.Font = Enum.Font.Roboto
        Btn.TextSize = 14
        Btn.TextColor3 = Color3.fromRGB(255, 255, 255)
        Btn.MouseButton1Click:Connect(Callback or function() end)

        local Corner = Instance.new("UICorner", Btn)
        Corner.CornerRadius = UDim.new(0, 6)
    end

    function Panel:Label(Text)
        local Lbl = Instance.new("TextLabel")
        Lbl.Parent = Scrolling
        Lbl.Size = UDim2.new(0, 120, 0, 40)
        Lbl.BackgroundColor3 = Color3.fromRGB(30, 30, 30)
        Lbl.Text = Text
        Lbl.Font = Enum.Font.Roboto
        Lbl.TextSize = 14
        Lbl.TextColor3 = Color3.fromRGB(255, 255, 255)

        local Corner = Instance.new("UICorner", Lbl)
        Corner.CornerRadius = UDim.new(0, 6)
    end

    -- Ejemplo de Slider básico horizontal
    function Panel:Slider(Text, Min, Max, Default, Callback)
        local Holder = Instance.new("Frame")
        Holder.Parent = Scrolling
        Holder.Size = UDim2.new(0, 150, 0, 40)
        Holder.BackgroundColor3 = Color3.fromRGB(30, 30, 30)

        local Label = Instance.new("TextLabel")
        Label.Parent = Holder
        Label.Size = UDim2.new(1, 0, 0.3, 0)
        Label.Text = Text
        Label.TextColor3 = Color3.new(1,1,1)
        Label.BackgroundTransparency = 1
        Label.Font = Enum.Font.Roboto
        Label.TextSize = 12

        local Slider = Instance.new("TextButton")
        Slider.Parent = Holder
        Slider.Position = UDim2.new(0.05, 0, 0.5, 0)
        Slider.Size = UDim2.new(0.9, 0, 0.3, 0)
        Slider.BackgroundColor3 = Color3.fromRGB(60, 60, 60)
        Slider.Text = tostring(Default or Min)

        local Value = Default or Min
        Slider.MouseButton1Click:Connect(function()
            Value = Value + 1
            if Value > Max then Value = Min end
            Slider.Text = tostring(Value)
            if Callback then Callback(Value) end
        end)

        local Corner = Instance.new("UICorner", Holder)
        Corner.CornerRadius = UDim.new(0, 6)
    end

    return Panel
end

return Lib
