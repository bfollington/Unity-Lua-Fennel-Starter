(fn init [id] 
    (set _G.State {
        :time 0
        :id id
    })
)

(fn update [id] 
    (set _G.State.time (- _G.State.time deltaTime))
    (let [x (math.sin (* 1 _G.State.time))
          y (math.cos _G.State.time)
          z (math.sin _G.State.time)]
        (GameObject:position x y z)
    )
)

;; hello world

(set _G.obj {: init : update})