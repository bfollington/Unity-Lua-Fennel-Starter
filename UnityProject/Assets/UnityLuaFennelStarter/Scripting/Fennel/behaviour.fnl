(fn _G.init [id] 
    {
        :time 0
    }
)

(fn _G.receive [id msg data state]
    (match msg
        :reset (set state.time 0)
    )
)

(fn _G.update [id state] 
    (set state.time (+ state.time (* deltaTime state.speed)))
    (let [x (math.sin (* 1 state.time))
          y (math.cos state.time)
          z (math.sin state.time)]
        (GameObject:position x y z)
    )
)
